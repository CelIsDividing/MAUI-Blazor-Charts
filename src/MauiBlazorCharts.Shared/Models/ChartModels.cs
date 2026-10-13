using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiBlazorCharts.Models;

public class DataPoint
{
    public string Label { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

public enum ChartType
{
    Bar,
    Pie,
    RoundGauge,
    HistoricalPlot
}

public partial class ChartItem : ObservableObject
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ChartType Type { get; set; }

    private double _currentValue;
    public double CurrentValue
    {
        get => _currentValue;
        set => SetProperty(ref _currentValue, value);
    }

    public double MinValue { get; set; } = 0;
    public double MaxValue { get; set; } = 100;

    private List<DataPoint> _historicalData = new();
    public List<DataPoint> HistoricalData
    {
        get => _historicalData;
        set => SetProperty(ref _historicalData, value);
    }
}
