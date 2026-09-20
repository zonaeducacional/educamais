using Avalonia;
using System;

namespace EducaMais;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) 
    {
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            System.IO.File.AppendAllText("/tmp/educamais_crash.log", $"\n\n[AppDomain] Crash: {e.ExceptionObject}");
        };
        System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            System.IO.File.AppendAllText("/tmp/educamais_crash.log", $"\n\n[TaskScheduler] Crash: {e.Exception}");
        };

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
#endif
            .WithInterFont()
            .LogToTrace();
}
