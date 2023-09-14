using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Kite.Views
{
    public class ViewBase<T> : UserControl where T : class
    {
        public T ViewModel => (T)DataContext!;

        public ViewBase() 
        {
            this.DataContext = Ioc.Default.GetService<T>();
        }
    }
}
