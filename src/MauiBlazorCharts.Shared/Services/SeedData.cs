using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// Seed data for the charts. Returns a new list on every call
/// so it can be safely used for both initialization and reset.
/// </summary>
public static class SeedData
{
    public static List<ChartItem> CreateCharts()
    {
        var now = DateTime.Now;

        return new List<ChartItem>
        {
            new ChartItem
            {
                Id = "chart-bar-1",
                Title = "Southern Region Sales",
                Description = "Number of units sold (SQLite)",
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
                    new() { Label = "May", Value = 45000, Timestamp = now }
                }
            },
            new ChartItem
            {
                Id = "chart-pie-2",
                Title = "Market Share",
                Description = "Percentage share in the industry (SQLite)",
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
            new ChartItem
            {
                Id = "chart-gauge-3",
                Title = "Service Quality",
                Description = "Customer satisfaction percentage (SQLite)",
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
            new ChartItem
            {
                Id = "chart-plot-4",
                Title = "User Trend",
                Description = "Active users over time (SQLite)",
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
        };
    }
}
