using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// Servis koji pruža test podatke za kartice
/// </summary>
public class DataService
{
    public static List<ChartItem> GetInitialCharts()
    {
        var random = new Random(42);

        return new List<ChartItem>
        {
            new ChartItem
            {
                Title = "Prodaja Južnih Regiona",
                Description = "Broj jedinica prodanih",
                Type = ChartType.Bar,
                CurrentValue = 45000,
                MaxValue = 100000,
                HistoricalData = GenerateHistoricalData(10, () => random.NextDouble() * 50000)
            },
            new ChartItem
            {
                Title = "Tržišni Udeo",
                Description = "Procentualni udeo u industriji",
                Type = ChartType.Pie,
                CurrentValue = 35,
                MaxValue = 100,
                HistoricalData = GenerateHistoricalData(5, () => random.NextDouble() * 100)
            },
            new ChartItem
            {
                Title = "Kvalitet Servisa",
                Description = "Procenat zadovoljstva korisnika",
                Type = ChartType.RoundGauge,
                CurrentValue = 87.5,
                MaxValue = 100,
                HistoricalData = GenerateHistoricalData(8, () => 75 + random.NextDouble() * 25)
            },
            new ChartItem
            {
                Title = "Trend Korisnika",
                Description = "Aktivnih korisnika tokom vremena",
                Type = ChartType.HistoricalPlot,
                CurrentValue = 5250,
                MaxValue = 10000,
                HistoricalData = GenerateHistoricalData(12, () => 1000 + random.NextDouble() * 8000)
            }
        };
    }

    public static List<DataPoint> GenerateHistoricalData(int count, Func<double> valueGenerator)
    {
        var data = new List<DataPoint>();
        var now = DateTime.Now;

        for (int i = count - 1; i >= 0; i--)
        {
            data.Add(new DataPoint
            {
                Label = $"T-{i}",
                Value = valueGenerator(),
                Timestamp = now.AddHours(-i)
            });
        }

        return data;
    }

    public static List<DataPoint> GetMoreData(string chartId)
    {
        var random = new Random();
        return GenerateHistoricalData(5, () => random.NextDouble() * 100000);
    }
}
