using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using App.ViewModels;
using App.ViewModels.Auth;
using App.Views;
using App.Views.Auth;

namespace App;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new LoginWindow
            {
                DataContext = new LoginWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}