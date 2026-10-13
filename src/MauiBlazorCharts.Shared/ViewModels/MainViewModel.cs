using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiBlazorCharts.Models;
using MauiBlazorCharts.Services;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace MauiBlazorCharts.ViewModels;

/// <summary>
/// Main ViewModel that manages the UI state and communicates exclusively through the Messenger (loose coupling).
/// </summary>
public partial class MainViewModel : ObservableObject,
    IRecipient<ChartUpdatedMessage>,
    IRecipient<AllChartsRefreshedMessage>
{
    private readonly Timer _autoRefreshTimer;
    private readonly IUiDispatcher _dispatcher;

#pragma warning disable MVVMTK0045

    [ObservableProperty]
    private ObservableCollection<ChartItem> _charts = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = "Initializing SQLite database...";

#pragma warning restore MVVMTK0045

    public MainViewModel(IUiDispatcher dispatcher)
    {
        _dispatcher = dispatcher;

        WeakReferenceMessenger.Default.Register<ChartUpdatedMessage>(this);
        WeakReferenceMessenger.Default.Register<AllChartsRefreshedMessage>(this);

        _autoRefreshTimer = new Timer(10000)
        {
            AutoReset = true,
            Enabled = false
        };

        _autoRefreshTimer.Elapsed += (_, _) =>
        {
            _dispatcher.Dispatch(async () =>
            {
                await RefreshAllCharts();
            });
        };
    }

    private void StartAutoRefresh()
    {
        if (!_autoRefreshTimer.Enabled)
        {
            _autoRefreshTimer.Start();
        }
    }

    private void StopAutoRefresh()
    {
        if (_autoRefreshTimer.Enabled)
        {
            _autoRefreshTimer.Stop();
        }
    }

    [RelayCommand]
    public async Task Initialize()
    {
        try
        {
            IsLoading = true;
            StatusMessage = "Loading data from the SQLite database...";

            var initialCharts = await WeakReferenceMessenger.Default
                .Send(new LoadChartsRequestMessage())
                .Response;

            Charts.Clear();
            foreach (var chart in initialCharts)
            {
                Charts.Add(chart);
            }

            StatusMessage = $"Successfully loaded {Charts.Count} charts from the SQLite database!";
            IsLoading = false;

            StartAutoRefresh();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading: {ex.Message}";
            IsLoading = false;
        }
    }

    [RelayCommand]
    public void IncreaseValue(ChartItem? chart)
    {
        if (chart == null) return;

        double delta = (chart.MaxValue - chart.MinValue) * 0.1;
        double newValue = Math.Min(chart.CurrentValue + delta, chart.MaxValue);

        WeakReferenceMessenger.Default.Send(new UpdateChartValueMessage(chart.Id, newValue));
    }

    [RelayCommand]
    public void DecreaseValue(ChartItem? chart)
    {
        if (chart == null) return;

        double delta = (chart.MaxValue - chart.MinValue) * 0.1;
        double newValue = Math.Max(chart.CurrentValue - delta, chart.MinValue);

        WeakReferenceMessenger.Default.Send(new UpdateChartValueMessage(chart.Id, newValue));
    }

    [RelayCommand]
    public void ResetChart(ChartItem? chart)
    {
        if (chart == null) return;

        WeakReferenceMessenger.Default.Send(new ResetChartMessage(chart.Id));
    }

    [RelayCommand]
    public async Task RefreshAllCharts()
    {
        try
        {
            await WeakReferenceMessenger.Default
                .Send(new RefreshAllChartsRequestMessage())
                .Response;

            StatusMessage = "All cards have been refreshed and synchronized with the SQLite database!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error refreshing: {ex.Message}";
        }
    }

    public void Receive(ChartUpdatedMessage message)
    {
        var existing = Charts.FirstOrDefault(c => c.Id == message.Chart.Id);
        if (existing != null)
        {
            existing.CurrentValue = message.Chart.CurrentValue;
            existing.HistoricalData = new List<DataPoint>(message.Chart.HistoricalData);
            StatusMessage = $"Chart '{existing.Title}' updated to {existing.CurrentValue:F1} (SQLite)";
        }
    }

    public void Receive(AllChartsRefreshedMessage message)
    {
        foreach (var updated in message.Charts)
        {
            var existing = Charts.FirstOrDefault(c => c.Id == updated.Id);
            if (existing != null)
            {
                existing.CurrentValue = updated.CurrentValue;
                existing.HistoricalData = new List<DataPoint>(updated.HistoricalData);
            }
        }
    }

    public void Dispose()
    {
        StopAutoRefresh();
        _autoRefreshTimer.Dispose();
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }
}
