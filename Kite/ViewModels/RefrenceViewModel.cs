using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using Material.Icons.Avalonia;
using Avalonia.Threading;
using System.Timers;
using Avalonia.Controls;
using Kite.Services;
using CommunityToolkit.Mvvm.Messaging;
using Kite.Messages;
using Kite.Views;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.DependencyInjection;
using DialogHostAvalonia;
using Kite.Views.Popups;

namespace Kite.ViewModels
{
    public partial class RefrenceViewModel : ViewModelBase, INavigateVM
    {
        DispatcherTimer timer;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(State))]
        [NotifyPropertyChangedFor(nameof(BlurAmount))]
        bool paused;

        MaterialIconKind State 
        {
            get 
            {
                return Paused ? MaterialIconKind.Play : MaterialIconKind.Pause;
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TimeLeftText))]
        int timeLeft;

        string TimeLeftText => TimeSpan.FromSeconds(TimeLeft)
            .ToString()
            .Substring(3);

        [ObservableProperty]
        int maxTime = 3;

        [ObservableProperty]
        string turnText;

        [ObservableProperty]
        Bitmap imageSource;

        [ObservableProperty]
        string text;

        double BlurAmount => Paused ? 10 : 0;

        [ObservableProperty]
        MoreMenuViewModel moreMenuViewModel;
        readonly RefrenceSrevice refrenceSrevice;
        readonly MainWindowViewModel mainWindowViewModel;

        public RefrenceViewModel(RefrenceSrevice refrenceSrevice, MainWindowViewModel mainWindowViewModel) 
        {
            this.refrenceSrevice = refrenceSrevice;
            this.mainWindowViewModel = mainWindowViewModel;
            moreMenuViewModel = new(this, refrenceSrevice);

            this.refrenceSrevice.refrenceChanged += (i) =>
            {
                TurnText = $"{i + 1}/{this.refrenceSrevice.ItemsCount}";
            };
        }

        void Tick(object? sender, EventArgs e)
        {
            TimeLeft--;
            if (TimeLeft < 0)
            {
                Next();
            }
        }

        public void Start(string path) 
        {
            ImageSource = ImageHelper.LoadFromResource(new(path));
            TimeLeft = MaxTime;
            Pause();
            Text = "Click to Start";
        }

        public void TogglePause()
        {
            if (Paused)
                Resume();
            else
                Pause();
        }

        public void Pause() 
        {
            Text = "Resume";
            Paused = true;
            timer.Stop();
        }

        public void Resume()
        {
            Text = "Pause";
            Paused = false;
            timer.Start();
        }

        public void GoToHome() 
        {
            WeakReferenceMessenger.Default.Send(new NavigateMessage(new HomeView()));
        }

        public void Enter()
        {
            Text = "Click to Start";

            TimeLeft = MaxTime;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Tick;
            Paused = true;
            
            ImageSource = ImageHelper.LoadFromResource(refrenceSrevice.GetNext().AsT0);
        }

        public void Exit()
        {
            timer.Tick -= Tick;
        }

        public void Next()
        {
            refrenceSrevice.GetNext().Switch(
                path => Start(path),
                over =>
                {
                    TimeLeft = 0;
                    Pause();
                    DialogHost.Show(Ioc.Default.GetRequiredService<FinishedView>());
                }
            );
        }

        public void Previous()
        {
            refrenceSrevice.GetPrevious().Switch(
                path => Start(path),
                over => { }
            );
        }

        public void Reset()
        {
            Pause();
            TimeLeft = MaxTime;
        }
    }
}
