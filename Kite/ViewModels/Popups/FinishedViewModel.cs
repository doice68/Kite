using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using DialogHostAvalonia;
using Kite.Messages;
using Kite.Services;
using Kite.Views;

namespace Kite.ViewModels.Popups
{
    public class FinishedViewModel : ViewModelBase
    {
        readonly MainWindowViewModel mainWindowViewModel;
        readonly RefrenceSrevice refrenceSrevice;

        public FinishedViewModel(MainWindowViewModel mainWindowViewModel, RefrenceSrevice refrenceSrevice)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.refrenceSrevice = refrenceSrevice;
            mainWindowViewModel.DialogHostCloseOnClick = false;
        }

        public void GoToHome() 
        {
            DialogHost.Close("main");
            mainWindowViewModel.DialogHostCloseOnClick = true;
            WeakReferenceMessenger.Default.Send<NavigateMessage>(
                new NavigateMessage(Ioc.Default.GetRequiredService<HomeView>()));
        }

        public void StartAgain() 
        {
            DialogHost.Close("main");
            mainWindowViewModel.DialogHostCloseOnClick = true;
            refrenceSrevice.StartOver();
            WeakReferenceMessenger.Default.Send<NavigateMessage>(
                new NavigateMessage(Ioc.Default.GetRequiredService<RefrenceView>()));
        }
    }
}
