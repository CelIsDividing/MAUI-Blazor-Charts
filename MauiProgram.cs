using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;
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

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddSingleton<DataService>();
        builder.Services.AddSingleton<MainViewModel>();

        var app = builder.Build();

        // Inicijalizuj DataService kako bi se registrovali Messenger handleri
        _ = app.Services.GetRequiredService<DataService>();

        return app;
    }
}
