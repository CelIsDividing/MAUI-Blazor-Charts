using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MauiBlazorCharts.Pages;
using MauiBlazorCharts.Services;
using MauiBlazorCharts.ViewModels;
using MauiBlazorCharts.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register web-specific (Blazor WebAssembly) store and dispatcher.
builder.Services.AddSingleton<IChartStore, InMemoryChartStore>();
builder.Services.AddSingleton<IUiDispatcher, WebUiDispatcher>();

// The same services and ViewModel as in the MAUI application.
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<MainViewModel>();

var host = builder.Build();

// Initialize DataService so that the Messenger handlers are registered before the UI sends a request.
_ = host.Services.GetRequiredService<DataService>();

await host.RunAsync();
