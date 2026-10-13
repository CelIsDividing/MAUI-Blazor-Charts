using MauiBlazorCharts.Services;

namespace MauiBlazorCharts.Web.Services;

/// <summary>
/// Web implementation of the UI dispatcher. Blazor WebAssembly executes code on a single thread,
/// so it is enough to invoke the action directly.
/// </summary>
public sealed class WebUiDispatcher : IUiDispatcher
{
    public void Dispatch(Action action) => action();
}
