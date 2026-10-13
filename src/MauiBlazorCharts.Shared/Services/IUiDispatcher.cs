namespace MauiBlazorCharts.Services;

/// <summary>
/// Abstraction of the UI thread. The MAUI implementation uses MainThread,
/// while the web (Blazor WebAssembly) implementation executes directly since the runtime is single-threaded.
/// </summary>
public interface IUiDispatcher
{
    void Dispatch(Action action);
}
