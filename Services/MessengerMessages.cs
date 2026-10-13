using CommunityToolkit.Mvvm.Messaging.Messages;
using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// Asinhroni zahtev za učitavanje svih grafikona iz SQLite baze.
/// </summary>
public class LoadChartsRequestMessage : AsyncRequestMessage<List<ChartItem>>
{
}

/// <summary>
/// Zahtev za ažuriranje trenutne vrednosti grafikona i dodavanje nove tačke u SQLite bazu.
/// </summary>
public class UpdateChartValueMessage
{
    public string ChartId { get; }
    public double NewValue { get; }

    public UpdateChartValueMessage(string chartId, double newValue)
    {
        ChartId = chartId;
        NewValue = newValue;
    }
}

/// <summary>
/// Zahtev za resetovanje vrednosti i istorije grafikona u SQLite bazi.
/// </summary>
public class ResetChartMessage
{
    public string ChartId { get; }

    public ResetChartMessage(string chartId)
    {
        ChartId = chartId;
    }
}

/// <summary>
/// Zahtev za osvežavanje svih grafikona u SQLite bazi novim podacima.
/// </summary>
public class RefreshAllChartsRequestMessage : AsyncRequestMessage<List<ChartItem>>
{
}

/// <summary>
/// Notifikacija koja se šalje nakon uspešnog ažuriranja pojedinačnog grafikona u bazi.
/// </summary>
public class ChartUpdatedMessage
{
    public ChartItem Chart { get; }

    public ChartUpdatedMessage(ChartItem chart)
    {
        Chart = chart;
    }
}

/// <summary>
/// Notifikacija koja se šalje nakon što su svi grafikoni ažurirani u bazi.
/// </summary>
public class AllChartsRefreshedMessage
{
    public List<ChartItem> Charts { get; }

    public AllChartsRefreshedMessage(List<ChartItem> charts)
    {
        Charts = charts;
    }
}

/// <summary>
/// Notifikacija o promeni vrednosti grafikona.
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
/// Notifikacija o promeni istorijskih podataka.
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
/// Globalni signal za osvežavanje UI prikaza.
/// </summary>
public class RefreshAllChartsMessage
{
    public DateTime RefreshTime { get; set; } = DateTime.Now;
}
