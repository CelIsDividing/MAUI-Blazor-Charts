using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiBlazorCharts.Models;
using MauiBlazorCharts.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace MauiBlazorCharts.ViewModels;

/// <summary>
/// Glavni ViewModel koji upravlja stanjem UI-ja i komunicira isključivo preko Messenger-a (slaba sprega).
/// </summary>
public partial class MainViewModel : ObservableObject,
    IRecipient<ChartUpdatedMessage>,
    IRecipient<AllChartsRefreshedMessage>
{
    private readonly Timer _autoRefreshTimer;

#pragma warning disable MVVMTK0045

    [ObservableProperty]
    private ObservableCollection<ChartItem> _charts = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = "Inicijalizacija SQLite baze...";

#pragma warning restore MVVMTK0045

    public MainViewModel()
    {
        WeakReferenceMessenger.Default.Register<ChartUpdatedMessage>(this);
        WeakReferenceMessenger.Default.Register<AllChartsRefreshedMessage>(this);

        _autoRefreshTimer = new Timer(10000)
        {
            AutoReset = true,
            Enabled = false
        };

        _autoRefreshTimer.Elapsed += (_, _) =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
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
            StatusMessage = "Učitavanje podataka iz SQLite baze...";

            var initialCharts = await WeakReferenceMessenger.Default
                .Send(new LoadChartsRequestMessage())
                .Response;

            Charts.Clear();
            foreach (var chart in initialCharts)
            {
                Charts.Add(chart);
            }

            StatusMessage = $"Uspešno učitano {Charts.Count} grafikona iz SQLite baze!";
            IsLoading = false;

            StartAutoRefresh();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Greška pri učitavanju: {ex.Message}";
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

            StatusMessage = "Sve kartice su osvežene i sinhronizovane sa SQLite bazom!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Greška pri osvežavanju: {ex.Message}";
        }
    }

    public void Receive(ChartUpdatedMessage message)
    {
        var existing = Charts.FirstOrDefault(c => c.Id == message.Chart.Id);
        if (existing != null)
        {
            existing.CurrentValue = message.Chart.CurrentValue;
            existing.HistoricalData = new List<DataPoint>(message.Chart.HistoricalData);
            StatusMessage = $"Grafikon '{existing.Title}' ažuriran na {existing.CurrentValue:F1} (SQLite)";
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
