using System;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Kite.Messages;
using Kite.ViewModels;
using Kite.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Kite;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        var bootstrapper = new Bootstrapper();
        var serviceProvider = bootstrapper.Register();

        Ioc.Default.ConfigureServices(serviceProvider.BuildServiceProvider());

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = Ioc.Default.GetRequiredService<MainWindow>();
            window.DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
            desktop.MainWindow = window;
            desktop.Exit += Desktop_Exit;
        }
        //else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        //{
        //    var window = Ioc.Default.GetRequiredService<MainWindow>();
        //    singleViewPlatform.MainView = window;
        //}

        base.OnFrameworkInitializationCompleted();
    }

    private void Desktop_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        WeakReferenceMessenger.Default.Send<ShutdownMessage>();

    }

    void Desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send<ShutdownMessage>();
    }
}
