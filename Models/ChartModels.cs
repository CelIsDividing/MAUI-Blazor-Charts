namespace MauiBlazorCharts.Models;

/// Predstavlja jednu podatkovnu tačku sa vrednostima
public class DataPoint
{
    public string Label { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

/// Predstavlja tip grafikona koji će biti prikazan
public enum ChartType
{
    Bar,
    Pie,
    RoundGauge,
    HistoricalPlot
}

/// Predstavlja jednu karticu sa graficima povezanu na podatke
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
