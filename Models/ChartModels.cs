namespace MauiBlazorCharts.Models;

/// <summary>
/// Predstavlja jednu podatkovnu tačku sa vrednostima
/// </summary>
public class DataPoint
{
    public string Label { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

/// <summary>
/// Predstavlja tip grafikona koji će biti prikazan
/// </summary>
public enum ChartType
{
    Bar,
    Pie,
    RoundGauge,
    HistoricalPlot
}

/// <summary>
/// Predstavlja jednu karticu sa graficima povezanu na podatke
/// </summary>
public class ChartItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ChartType Type { get; set; }
    public double CurrentValue { get; set; }
    public double MinValue { get; set; } = 0;
    public double MaxValue { get; set; } = 100;
    public List<DataPoint> HistoricalData { get; set; } = new();
}
