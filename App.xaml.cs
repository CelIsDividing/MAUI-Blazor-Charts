using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.Controls;

namespace MauiBlazorCharts;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var webview = new BlazorWebView
        {
            HostPage = "wwwroot/index.html"
        };

        webview.RootComponents.Add(new RootComponent
        {
            Selector = "#app",
            ComponentType = typeof(global::MauiBlazorCharts.Pages.App)
        });

        var page = new ContentPage
        {
            Content = webview
        };

        return new Window(page)
        {
            Title = "MauiBlazorCharts"
        };
    }
}
