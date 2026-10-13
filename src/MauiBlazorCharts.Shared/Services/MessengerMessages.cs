using CommunityToolkit.Mvvm.Messaging.Messages;
using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// Asynchronous request to load all charts from the SQLite database.
/// </summary>
public class LoadChartsRequestMessage : AsyncRequestMessage<List<ChartItem>>
{
}

/// <summary>
/// Request to update the current value of a chart and add a new data point to the SQLite database.
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
/// Request to reset a chart's value and history in the SQLite database.
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
/// Request to refresh all charts in the SQLite database with new data.
/// </summary>
public class RefreshAllChartsRequestMessage : AsyncRequestMessage<List<ChartItem>>
{
}

/// <summary>
/// Notification sent after a single chart is successfully updated in the database.
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
/// Notification sent after all charts have been updated in the database.
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
/// Notification about a chart value change.
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
/// Notification about a change in historical data.
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
/// Global signal to refresh the UI.
/// </summary>
public class RefreshAllChartsMessage
{
    public DateTime RefreshTime { get; set; } = DateTime.Now;
}
