using Microsoft.JSInterop;

namespace BlazorPdf;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class BlazorPdfJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public BlazorPdfJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorPdf/js/bundle.js").AsTask());
    }

    public async ValueTask SayHello()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("HelloWorld");
    }

    public async ValueTask GeneratePdf()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("GeneratePdf");
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
