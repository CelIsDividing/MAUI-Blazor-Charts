using Microsoft.EntityFrameworkCore;
using MauiBlazorCharts.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiBlazorCharts.Data;

public class ChartEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ChartType Type { get; set; }
    public double CurrentValue { get; set; }
    public double MinValue { get; set; } = 0;
    public double MaxValue { get; set; } = 100;

    public List<DataPointEntity> HistoricalData { get; set; } = new();
}

public class DataPointEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ChartEntityId { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;

    [ForeignKey(nameof(ChartEntityId))]
    public ChartEntity? Chart { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<ChartEntity> Charts => Set<ChartEntity>();
    public DbSet<DataPointEntity> DataPoints => Set<DataPointEntity>();

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string dbFolder = FileSystem.AppDataDirectory;
            if (!Directory.Exists(dbFolder))
            {
                Directory.CreateDirectory(dbFolder);
            }
            string dbPath = Path.Combine(dbFolder, "charts.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChartEntity>()
            .HasMany(c => c.HistoricalData)
            .WithOne(d => d.Chart)
            .HasForeignKey(d => d.ChartEntityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
