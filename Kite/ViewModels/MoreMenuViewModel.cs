using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kite.Services;

namespace Kite.ViewModels
{
    public partial class MoreMenuViewModel : ViewModelBase
    {
        [ObservableProperty]
        bool isFirst;
        [ObservableProperty]
        bool isLast;

        readonly RefrenceViewModel refrenceViewModel;
        readonly RefrenceSrevice refrenceSrevice;

        public MoreMenuViewModel(RefrenceViewModel refrenceViewModel, RefrenceSrevice refrenceSrevice)
        {
            this.refrenceViewModel = refrenceViewModel;
            this.refrenceSrevice = refrenceSrevice;

            refrenceSrevice.refrenceChanged += (i) => 
            {
                IsFirst = this.refrenceSrevice.IsFirst();
                IsLast = this.refrenceSrevice.IsLast();
            };
        }

        public void Next() => refrenceViewModel.Next();
        public void Previous() => refrenceViewModel.Previous();
        public void Reset() => refrenceViewModel.Reset();

        public void ExtraTime(object sec) 
        {
            refrenceViewModel.TimeLeft += int.Parse(sec as string);
        }
    }
}
