using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Kite.Messages;
using Kite.Views;

namespace Kite.ViewModels;

public partial class MainViewModel : ViewModelBase, IRecipient<NavigateMessage>
{
    [ObservableProperty]
    UserControl page;

    public MainViewModel()
    {
        Page = Ioc.Default.GetRequiredService<HomeView>();
        WeakReferenceMessenger.Default.Register(this);
    }

    void IRecipient<NavigateMessage>.Receive(NavigateMessage message)
    {
        if (Page.DataContext is INavigateVM np)
            np.Exit();

        Page = message.Page;

        if (Page.DataContext is INavigateVM np2)
            np2.Enter();
    }
}
