using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Input.Platform;
using Kite.ViewModels;

namespace Kite.Views;

public partial class MainView : ViewBase<MainViewModel>
{
    public static WindowNotificationManager? manager;

    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        var topLevel = TopLevel.GetTopLevel(this);
        manager = new WindowNotificationManager(topLevel) { MaxItems = 3, Margin = new Thickness(0, 40) };
    }
}
