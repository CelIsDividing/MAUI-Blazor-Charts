using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MauiBlazorCharts.ViewModels;
using MauiBlazorCharts.Services;
using MauiBlazorCharts.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register services
builder.Services.AddScoped<DataService>();
builder.Services.AddSingleton<MainViewModel>();

await builder.Build().RunAsync();
