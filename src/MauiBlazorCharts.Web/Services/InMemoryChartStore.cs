using MauiBlazorCharts.Models;
using MauiBlazorCharts.Services;

namespace MauiBlazorCharts.Web.Services;

/// <summary>
/// Web (Blazor WebAssembly) implementation of the store that keeps charts in memory.
/// Data is reset when the page is refreshed; can be replaced with an IndexedDB/localStorage implementation.
/// </summary>
public sealed class InMemoryChartStore : IChartStore
{
    private readonly Dictionary<string, ChartItem> _store = new();

    public Task InitializeAsync() => Task.CompletedTask;

    public Task<List<ChartItem>> LoadAsync()
        => Task.FromResult(_store.Values.OrderBy(c => c.Id).Select(Clone).ToList());

    public Task UpsertAsync(ChartItem chart)
    {
        _store[chart.Id] = Clone(chart);
        return Task.CompletedTask;
    }

    public async Task UpsertAllAsync(IEnumerable<ChartItem> charts)
    {
        foreach (var chart in charts)
        {
            await UpsertAsync(chart);
        }
    }

    private static ChartItem Clone(ChartItem source) => new()
    {
        Id = source.Id,
        Title = source.Title,
        Description = source.Description,
        Type = source.Type,
        CurrentValue = source.CurrentValue,
        MinValue = source.MinValue,
        MaxValue = source.MaxValue,
        HistoricalData = source.HistoricalData
            .Select(p => new DataPoint { Label = p.Label, Value = p.Value, Timestamp = p.Timestamp })
            .ToList()
    };
}
