using CommunityToolkit.Mvvm.Messaging;
using MauiBlazorCharts.Data;
using MauiBlazorCharts.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiBlazorCharts.Services;

public class DataService :
    IRecipient<LoadChartsRequestMessage>,
    IRecipient<UpdateChartValueMessage>,
    IRecipient<ResetChartMessage>,
    IRecipient<RefreshAllChartsRequestMessage>
{
    public DataService()
    {
        InitializeDatabase();
        WeakReferenceMessenger.Default.Register<LoadChartsRequestMessage>(this);
        WeakReferenceMessenger.Default.Register<UpdateChartValueMessage>(this);
        WeakReferenceMessenger.Default.Register<ResetChartMessage>(this);
        WeakReferenceMessenger.Default.Register<RefreshAllChartsRequestMessage>(this);
    }

    private void InitializeDatabase()
    {
        using var context = new AppDbContext();
        context.Database.EnsureCreated();

        if (!context.Charts.Any())
        {
            var now = DateTime.Now;
            context.Charts.AddRange(
                new ChartEntity
                {
                    Id = "chart-bar-1",
                    Title = "Prodaja Južnih Regiona",
                    Description = "Broj jedinica prodanih (SQLite)",
                    Type = ChartType.Bar,
                    CurrentValue = 45000,
                    MinValue = 0,
                    MaxValue = 100000,
                    HistoricalData = new()
                    {
                        new() { Label = "Jan", Value = 25000, Timestamp = now.AddMonths(-4) },
                        new() { Label = "Feb", Value = 32000, Timestamp = now.AddMonths(-3) },
                        new() { Label = "Mar", Value = 28000, Timestamp = now.AddMonths(-2) },
                        new() { Label = "Apr", Value = 41000, Timestamp = now.AddMonths(-1) },
                        new() { Label = "Maj", Value = 45000, Timestamp = now }
                    }
                },
                new ChartEntity
                {
                    Id = "chart-pie-2",
                    Title = "Tržišni Udeo",
                    Description = "Procentualni udeo u industriji (SQLite)",
                    Type = ChartType.Pie,
                    CurrentValue = 35,
                    MinValue = 0,
                    MaxValue = 100,
                    HistoricalData = new()
                    {
                        new() { Label = "Q1", Value = 22, Timestamp = now.AddMonths(-9) },
                        new() { Label = "Q2", Value = 27, Timestamp = now.AddMonths(-6) },
                        new() { Label = "Q3", Value = 31, Timestamp = now.AddMonths(-3) },
                        new() { Label = "Q4", Value = 35, Timestamp = now }
                    }
                },
                new ChartEntity
                {
                    Id = "chart-gauge-3",
                    Title = "Kvalitet Servisa",
                    Description = "Procenat zadovoljstva korisnika (SQLite)",
                    Type = ChartType.RoundGauge,
                    CurrentValue = 87.5,
                    MinValue = 0,
                    MaxValue = 100,
                    HistoricalData = new()
                    {
                        new() { Label = "T-4", Value = 80.0, Timestamp = now.AddHours(-4) },
                        new() { Label = "T-3", Value = 82.5, Timestamp = now.AddHours(-3) },
                        new() { Label = "T-2", Value = 85.0, Timestamp = now.AddHours(-2) },
                        new() { Label = "T-1", Value = 86.0, Timestamp = now.AddHours(-1) },
                        new() { Label = "T-0", Value = 87.5, Timestamp = now }
                    }
                },
                new ChartEntity
                {
                    Id = "chart-plot-4",
                    Title = "Trend Korisnika",
                    Description = "Aktivnih korisnika tokom vremena (SQLite)",
                    Type = ChartType.HistoricalPlot,
                    CurrentValue = 5250,
                    MinValue = 0,
                    MaxValue = 10000,
                    HistoricalData = new()
                    {
                        new() { Label = "T-4", Value = 3800, Timestamp = now.AddMinutes(-40) },
                        new() { Label = "T-3", Value = 4200, Timestamp = now.AddMinutes(-30) },
                        new() { Label = "T-2", Value = 4600, Timestamp = now.AddMinutes(-20) },
                        new() { Label = "T-1", Value = 4900, Timestamp = now.AddMinutes(-10) },
                        new() { Label = "T-0", Value = 5250, Timestamp = now }
                    }
                }
            );
            context.SaveChanges();
        }
    }

    public void Receive(LoadChartsRequestMessage message)
    {
        message.Reply(GetChartsAsync());
    }

    public async Task<List<ChartItem>> GetChartsAsync()
    {
        using var context = new AppDbContext();
        var entities = await context.Charts
            .Include(c => c.HistoricalData)
            .AsNoTracking()
            .ToListAsync();

        return entities.Select(MapToDomain).ToList();
    }

    public async void Receive(UpdateChartValueMessage message)
    {
        using var context = new AppDbContext();
        var entity = await context.Charts
            .Include(c => c.HistoricalData)
            .FirstOrDefaultAsync(c => c.Id == message.ChartId);

        if (entity != null)
        {
            entity.CurrentValue = message.NewValue;
            entity.HistoricalData.Add(new DataPointEntity
            {
                ChartEntityId = entity.Id,
                Label = $"T+{entity.HistoricalData.Count + 1}",
                Value = message.NewValue,
                Timestamp = DateTime.Now
            });

            await context.SaveChangesAsync();

            var domainItem = MapToDomain(entity);
            WeakReferenceMessenger.Default.Send(new ChartUpdatedMessage(domainItem));
            WeakReferenceMessenger.Default.Send(new ChartValueChangedMessage(entity.Id, entity.CurrentValue));
            WeakReferenceMessenger.Default.Send(new RefreshAllChartsMessage());
        }
    }

    public async void Receive(ResetChartMessage message)
    {
        using var context = new AppDbContext();
        var entity = await context.Charts
            .Include(c => c.HistoricalData)
            .FirstOrDefaultAsync(c => c.Id == message.ChartId);

        if (entity != null)
        {
            entity.CurrentValue = entity.Type switch
            {
                ChartType.Bar => 45000,
                ChartType.Pie => 35,
                ChartType.RoundGauge => 87.5,
                ChartType.HistoricalPlot => 5250,
                _ => entity.MaxValue * 0.5
            };

            context.DataPoints.RemoveRange(entity.HistoricalData);
            entity.HistoricalData.Clear();

            var now = DateTime.Now;
            if (entity.Type == ChartType.Bar)
            {
                entity.HistoricalData.AddRange(new[]
                {
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Jan", Value = 25000, Timestamp = now.AddMonths(-4) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Feb", Value = 32000, Timestamp = now.AddMonths(-3) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Mar", Value = 28000, Timestamp = now.AddMonths(-2) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Apr", Value = 41000, Timestamp = now.AddMonths(-1) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Maj", Value = 45000, Timestamp = now }
                });
            }
            else if (entity.Type == ChartType.Pie)
            {
                entity.HistoricalData.AddRange(new[]
                {
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Q1", Value = 22, Timestamp = now.AddMonths(-9) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Q2", Value = 27, Timestamp = now.AddMonths(-6) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Q3", Value = 31, Timestamp = now.AddMonths(-3) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "Q4", Value = 35, Timestamp = now }
                });
            }
            else if (entity.Type == ChartType.RoundGauge)
            {
                entity.HistoricalData.AddRange(new[]
                {
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-4", Value = 80.0, Timestamp = now.AddHours(-4) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-3", Value = 82.5, Timestamp = now.AddHours(-3) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-2", Value = 85.0, Timestamp = now.AddHours(-2) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-1", Value = 86.0, Timestamp = now.AddHours(-1) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-0", Value = 87.5, Timestamp = now }
                });
            }
            else
            {
                entity.HistoricalData.AddRange(new[]
                {
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-4", Value = 3800, Timestamp = now.AddMinutes(-40) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-3", Value = 4200, Timestamp = now.AddMinutes(-30) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-2", Value = 4600, Timestamp = now.AddMinutes(-20) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-1", Value = 4900, Timestamp = now.AddMinutes(-10) },
                    new DataPointEntity { ChartEntityId = entity.Id, Label = "T-0", Value = 5250, Timestamp = now }
                });
            }

            await context.SaveChangesAsync();

            var domainItem = MapToDomain(entity);
            WeakReferenceMessenger.Default.Send(new ChartUpdatedMessage(domainItem));
            WeakReferenceMessenger.Default.Send(new ChartValueChangedMessage(entity.Id, entity.CurrentValue));
            WeakReferenceMessenger.Default.Send(new RefreshAllChartsMessage());
        }
    }

    public void Receive(RefreshAllChartsRequestMessage message)
    {
        message.Reply(RefreshAllChartsAsync());
    }

    public async Task<List<ChartItem>> RefreshAllChartsAsync()
    {
        using var context = new AppDbContext();
        var entities = await context.Charts
            .Include(c => c.HistoricalData)
            .ToListAsync();

        var random = new Random();
        foreach (var entity in entities)
        {
            double delta = (random.NextDouble() - 0.5) * (entity.MaxValue * 0.1);
            double newValue = Math.Clamp(entity.CurrentValue + delta, entity.MinValue, entity.MaxValue);

            entity.CurrentValue = newValue;
            entity.HistoricalData.Add(new DataPointEntity
            {
                ChartEntityId = entity.Id,
                Label = $"T+{entity.HistoricalData.Count + 1}",
                Value = newValue,
                Timestamp = DateTime.Now
            });
        }

        await context.SaveChangesAsync();

        var updatedCharts = entities.Select(MapToDomain).ToList();
        WeakReferenceMessenger.Default.Send(new AllChartsRefreshedMessage(updatedCharts));
        WeakReferenceMessenger.Default.Send(new RefreshAllChartsMessage());

        return updatedCharts;
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
