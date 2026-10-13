using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// Abstraction of chart persistence. The MAUI implementation uses Entity Framework Core + SQLite,
/// while the web implementation keeps data in memory (can be replaced with an IndexedDB/localStorage implementation).
/// </summary>
public interface IChartStore
{
    Task InitializeAsync();

    Task<List<ChartItem>> LoadAsync();

    Task UpsertAsync(ChartItem chart);

    Task UpsertAllAsync(IEnumerable<ChartItem> charts);
}
