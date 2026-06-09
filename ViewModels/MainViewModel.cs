using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiBlazorCharts.Models;
using MauiBlazorCharts.Services;
using System.Collections.ObjectModel;

namespace MauiBlazorCharts.ViewModels;

/// <summary>
/// Glavni ViewModel koji upravlja svim karticama
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly DataService _dataService;

    [ObservableProperty]
    private ObservableCollection<ChartItem> charts = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string statusMessage = "Izvor podataka učitavan...";

    public MainViewModel()
    {
        _dataService = new DataService();
    }

    [RelayCommand]
    public async Task Initialize()
    {
        try
        {
            IsLoading = true;
            StatusMessage = "Učitavanje kartice...";

            // Učitaj početne podatke
            var initialCharts = DataService.GetInitialCharts();
            
            foreach (var chart in initialCharts)
            {
                Charts.Add(chart);
            }

            StatusMessage = "Kartice uspešno učitane!";
            IsLoading = false;

            // Registruj se na poruke
            WeakReferenceMessenger.Default.Register<ChartValueChangedMessage>(this, HandleChartValueChanged);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Greška: {ex.Message}";
            IsLoading = false;
        }
    }

    /// <summary>
    /// Povećaj vrednost kartice za 10%
    /// </summary>
    [RelayCommand]
    public void IncreaseValue(ChartItem? chart)
    {
        if (chart == null) return;

        double newValue = chart.CurrentValue + (chart.MaxValue * 0.1);
        if (newValue > chart.MaxValue)
            newValue = chart.MaxValue;

        chart.CurrentValue = newValue;
        chart.HistoricalData.Add(new DataPoint
        {
            Label = $"T+{chart.HistoricalData.Count}",
            Value = newValue,
            Timestamp = DateTime.Now
        });

        // Pošalji poruku svim slušačima
        WeakReferenceMessenger.Default.Send(
            new ChartValueChangedMessage(chart.Id, newValue));

        StatusMessage = $"Kartica '{chart.Title}' ažurirana na {newValue:F2}";
    }

    /// <summary>
    /// Smanji vrednost kartice za 10%
    /// </summary>
    [RelayCommand]
    public void DecreaseValue(ChartItem? chart)
    {
        if (chart == null) return;

        double newValue = chart.CurrentValue - (chart.MaxValue * 0.1);
        if (newValue < chart.MinValue)
            newValue = chart.MinValue;

        chart.CurrentValue = newValue;
        chart.HistoricalData.Add(new DataPoint
        {
            Label = $"T+{chart.HistoricalData.Count}",
            Value = newValue,
            Timestamp = DateTime.Now
        });

        // Pošalji poruku svim slušačima
        WeakReferenceMessenger.Default.Send(
            new ChartValueChangedMessage(chart.Id, newValue));

        StatusMessage = $"Kartica '{chart.Title}' smanjena na {newValue:F2}";
    }

    /// <summary>
    /// Resetuj vrednost kartice na početnu
    /// </summary>
    [RelayCommand]
    public void ResetChart(ChartItem? chart)
    {
        if (chart == null) return;

        chart.CurrentValue = chart.MaxValue * 0.5;
        chart.HistoricalData.Clear();
        chart.HistoricalData = DataService.GenerateHistoricalData(5, 
            () => new Random().NextDouble() * chart.MaxValue);

        WeakReferenceMessenger.Default.Send(
            new RefreshAllChartsMessage());

        StatusMessage = $"Kartica '{chart.Title}' resetovana";
    }

    /// <summary>
    /// Osveži sve kartice sa novim podacima
    /// </summary>
    [RelayCommand]
    public void RefreshAllCharts()
    {
        var random = new Random();
        foreach (var chart in Charts)
        {
            chart.CurrentValue = random.NextDouble() * chart.MaxValue;
            chart.HistoricalData.Add(new DataPoint
            {
                Label = $"T+{chart.HistoricalData.Count}",
                Value = chart.CurrentValue,
                Timestamp = DateTime.Now
            });
        }

        WeakReferenceMessenger.Default.Send(new RefreshAllChartsMessage());
        StatusMessage = "Sve kartice su osvežene!";
    }

    /// <summary>
    /// Rukuj promenom vrednosti kartice iz drugih komponenti
    /// </summary>
    private void HandleChartValueChanged(object recipient, ChartValueChangedMessage message)
    {
        var chart = Charts.FirstOrDefault(c => c.Id == message.ChartId);
        if (chart != null)
        {
            OnPropertyChanged(nameof(Charts));
        }
    }
}
