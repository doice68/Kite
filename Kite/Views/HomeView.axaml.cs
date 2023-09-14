using Avalonia.Controls;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.ComponentModel;
using Kite.ViewModels;

namespace Kite.Views
{
    public partial class HomeView : ViewBase<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();   
        }

        public void FolderChaged(object sender, SelectionChangedEventArgs args) 
        {
            //ViewModel.SelectFolder(((ListBox)sender).SelectedIndex);
            //((ListBox)sender).SelectedIndex = -1;
        }
    }
}
