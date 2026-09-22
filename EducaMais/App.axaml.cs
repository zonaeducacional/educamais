using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using EducaMais.ViewModels;
using EducaMais.Views;
using System;

namespace EducaMais;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        try
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(),
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel()
                };
            }
        }
        catch (Exception ex)
        {
            // Em WASM, exceções de startup deixam a tela vazia.
            // Mostramos o erro para facilitar diagnóstico.
            var errorView = new TextBlock
            {
                Text = $"ERRO DE STARTUP:\n{ex.GetType().Name}: {ex.Message}\n\n{ex.StackTrace}",
                Foreground = new SolidColorBrush(Colors.Red),
                Background = new SolidColorBrush(Colors.White),
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20),
            };

            if (ApplicationLifetime is ISingleViewApplicationLifetime sp)
                sp.MainView = errorView;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
