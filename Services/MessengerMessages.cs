using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// Poruka koja se šalje kada se vrednost neke kartice promeni
/// </summary>
public class ChartValueChangedMessage
{
    public string ChartId { get; set; }
    public double NewValue { get; set; }
    public DateTime ChangedAt { get; set; }

    public ChartValueChangedMessage(string chartId, double newValue)
    {
        ChartId = chartId;
        NewValue = newValue;
        ChangedAt = DateTime.Now;
    }
}

/// <summary>
/// Poruka koja se šalje kada se ponude nove istorijske podatke
/// </summary>
public class HistoricalDataUpdatedMessage
{
    public string ChartId { get; set; }
    public List<DataPoint> Data { get; set; }

    public HistoricalDataUpdatedMessage(string chartId, List<DataPoint> data)
    {
        ChartId = chartId;
        Data = data;
    }
}

/// <summary>
/// Poruka koja se šalje kada se sve kartice trebaju osveži
/// </summary>
public class RefreshAllChartsMessage
{
    public DateTime RefreshTime { get; set; } = DateTime.Now;
}
