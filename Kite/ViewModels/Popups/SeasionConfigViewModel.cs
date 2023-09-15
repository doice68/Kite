using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using DialogHostAvalonia;
using Kite.Messages;
using Kite.Services;
using Kite.Views;

namespace Kite.ViewModels.Popups
{
    public partial class SeasionConfigViewModel : ViewModelBase
    {
        [ObservableProperty]
        bool shuffle = true;

        [ObservableProperty]
        int time = 60;

        public string path;

        public void Start() 
        {
            // This sucks but i really want to be done w this project
            Ioc.Default.GetRequiredService<RefrenceSrevice>().Init(path, Shuffle);

            var rv = Ioc.Default.GetRequiredService<RefrenceView>();
            rv.ViewModel.MaxTime = Time;

            WeakReferenceMessenger.Default.Send(new NavigateMessage(rv));
            DialogHost.Close("main");
        }

        public void Close() 
        {
            DialogHost.Close("main");
        }
    }
}
