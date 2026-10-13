using MauiBlazorCharts.Data;
using MauiBlazorCharts.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiBlazorCharts.Services;

/// <summary>
/// SQLite (Entity Framework Core) implementation of the chart store for the MAUI application.
/// </summary>
public sealed class EfChartStore : IChartStore
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public EfChartStore(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task InitializeAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task<List<ChartItem>> LoadAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var entities = await context.Charts
            .Include(c => c.HistoricalData)
            .AsNoTracking()
            .ToListAsync();

        return entities.Select(MapToDomain).ToList();
    }

    public async Task UpsertAsync(ChartItem chart)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var entity = await context.Charts
            .Include(c => c.HistoricalData)
            .FirstOrDefaultAsync(c => c.Id == chart.Id);

        if (entity is null)
        {
            entity = new ChartEntity { Id = chart.Id };
            context.Charts.Add(entity);
        }

        entity.Title = chart.Title;
        entity.Description = chart.Description;
        entity.Type = chart.Type;
        entity.CurrentValue = chart.CurrentValue;
        entity.MinValue = chart.MinValue;
        entity.MaxValue = chart.MaxValue;

        context.DataPoints.RemoveRange(entity.HistoricalData);
        entity.HistoricalData = chart.HistoricalData
            .OrderBy(h => h.Timestamp)
            .Select(h => new DataPointEntity
            {
                ChartEntityId = chart.Id,
                Label = h.Label,
                Value = h.Value,
                Timestamp = h.Timestamp
            })
            .ToList();

        await context.SaveChangesAsync();
    }

    public async Task UpsertAllAsync(IEnumerable<ChartItem> charts)
    {
        foreach (var chart in charts)
        {
            await UpsertAsync(chart);
        }
    }

    private static ChartItem MapToDomain(ChartEntity entity)
    {
        return new ChartItem
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Type = entity.Type,
            CurrentValue = entity.CurrentValue,
            MinValue = entity.MinValue,
            MaxValue = entity.MaxValue,
            HistoricalData = entity.HistoricalData
                .OrderBy(h => h.Timestamp)
                .Select(h => new DataPoint
                {
                    Label = h.Label,
                    Value = h.Value,
                    Timestamp = h.Timestamp
                })
                .ToList()
        };
    }
}
