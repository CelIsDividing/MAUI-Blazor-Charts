using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// Poruka koja se šalje kada se vrednost neke kartice promeni
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

/// Poruka koja se šalje kada se ponude nove istorijske podatke
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

/// Poruka koja se šalje kada se sve kartice trebaju osveži
public class RefreshAllChartsMessage
{
    public DateTime RefreshTime { get; set; } = DateTime.Now;
}
