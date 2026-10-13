using CommunityToolkit.Mvvm.Messaging;
using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// Service that coordinates loading and modifying charts through the Messenger (loose coupling),
/// while delegating persistence to the <see cref="IChartStore"/> implementation (SQLite on MAUI, memory on web).
/// </summary>
public sealed class DataService :
    IRecipient<LoadChartsRequestMessage>,
    IRecipient<UpdateChartValueMessage>,
    IRecipient<ResetChartMessage>,
    IRecipient<RefreshAllChartsRequestMessage>
{
    private readonly IChartStore _store;
    private readonly List<ChartItem> _charts = new();
    private Task? _initTask;

    public DataService(IChartStore store)
    {
        _store = store;

        WeakReferenceMessenger.Default.Register<LoadChartsRequestMessage>(this);
        WeakReferenceMessenger.Default.Register<UpdateChartValueMessage>(this);
        WeakReferenceMessenger.Default.Register<ResetChartMessage>(this);
        WeakReferenceMessenger.Default.Register<RefreshAllChartsRequestMessage>(this);
    }

    private Task EnsureInitializedAsync() => _initTask ??= InitializeAsync();

    private async Task InitializeAsync()
    {
        await _store.InitializeAsync();

        var loaded = await _store.LoadAsync();
        if (loaded.Count == 0)
        {
            var seed = SeedData.CreateCharts();
            _charts.AddRange(seed);
            await _store.UpsertAllAsync(seed);
        }
        else
        {
            _charts.AddRange(loaded);
        }
    }

    public void Receive(LoadChartsRequestMessage message)
    {
        message.Reply(GetChartsAsync());
    }

    public async Task<List<ChartItem>> GetChartsAsync()
    {
        await EnsureInitializedAsync();
        return _charts.ToList();
    }

    public async void Receive(UpdateChartValueMessage message)
    {
        await EnsureInitializedAsync();

        var chart = _charts.FirstOrDefault(c => c.Id == message.ChartId);
        if (chart is null) return;

        chart.CurrentValue = message.NewValue;
        chart.HistoricalData.Add(new DataPoint
        {
            Label = $"T+{chart.HistoricalData.Count + 1}",
            Value = message.NewValue,
            Timestamp = DateTime.Now
        });

        await _store.UpsertAsync(chart);
        NotifyChartUpdated(chart);
    }

    public async void Receive(ResetChartMessage message)
    {
        await EnsureInitializedAsync();

        var chart = _charts.FirstOrDefault(c => c.Id == message.ChartId);
        if (chart is null) return;

        var fresh = SeedData.CreateCharts().First(c => c.Id == message.ChartId);
        chart.CurrentValue = fresh.CurrentValue;
        chart.HistoricalData = fresh.HistoricalData;

        await _store.UpsertAsync(chart);
        NotifyChartUpdated(chart);
    }

    public void Receive(RefreshAllChartsRequestMessage message)
    {
        message.Reply(RefreshAllChartsAsync());
    }

    public async Task<List<ChartItem>> RefreshAllChartsAsync()
    {
        await EnsureInitializedAsync();

        var random = new Random();
        foreach (var chart in _charts)
        {
            double delta = (random.NextDouble() - 0.5) * (chart.MaxValue * 0.1);
            double newValue = Math.Clamp(chart.CurrentValue + delta, chart.MinValue, chart.MaxValue);

            chart.CurrentValue = newValue;
            chart.HistoricalData.Add(new DataPoint
            {
                Label = $"T+{chart.HistoricalData.Count + 1}",
                Value = newValue,
                Timestamp = DateTime.Now
            });

            await _store.UpsertAsync(chart);
        }

        WeakReferenceMessenger.Default.Send(new AllChartsRefreshedMessage(_charts.ToList()));
        WeakReferenceMessenger.Default.Send(new RefreshAllChartsMessage());

        return _charts.ToList();
    }

    private static void NotifyChartUpdated(ChartItem chart)
    {
        WeakReferenceMessenger.Default.Send(new ChartUpdatedMessage(chart));
        WeakReferenceMessenger.Default.Send(new ChartValueChangedMessage(chart.Id, chart.CurrentValue));
        WeakReferenceMessenger.Default.Send(new RefreshAllChartsMessage());
    }
}
