using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.EntityFrameworkCore;
using MauiBlazorCharts.Data;
using MauiBlazorCharts.Services;
using MauiBlazorCharts.ViewModels;

namespace MauiBlazorCharts;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "charts.db");
        builder.Services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        builder.Services.AddSingleton<IChartStore, EfChartStore>();
        builder.Services.AddSingleton<IUiDispatcher, MauiUiDispatcher>();
        builder.Services.AddSingleton<DataService>();
        builder.Services.AddSingleton<MainViewModel>();

        var app = builder.Build();

        // Initialize DataService so that the Messenger handlers are registered
        _ = app.Services.GetRequiredService<DataService>();

        return app;
    }
}
