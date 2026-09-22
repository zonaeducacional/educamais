using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using EducaMais;

internal sealed partial class Program
{
    private static async Task Main(string[] args)
    {
        try 
        {
            Console.WriteLine("=== AVALONIA WASM STARTING ===");
            await BuildAvaloniaApp()
                .WithInterFont()
                .UseReactiveUI()
                .StartBrowserAppAsync("out");
        }
        catch (Exception ex)
        {
            Console.WriteLine("CRITICAL ERROR: " + ex.ToString());
        }
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}
