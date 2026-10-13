using MauiBlazorCharts.Models;

namespace MauiBlazorCharts.Services;

/// <summary>
/// MAUI/desktop implementation of the UI dispatcher based on MainThread.
/// </summary>
public sealed class MauiUiDispatcher : IUiDispatcher
{
    public void Dispatch(Action action)
    {
        if (MainThread.IsMainThread)
        {
            action();
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(action);
        }
    }
}
