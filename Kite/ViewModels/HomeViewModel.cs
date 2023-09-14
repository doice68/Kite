using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DialogHostAvalonia;
using Kite.Messages;
using Kite.Models;
using Kite.Services;
using Kite.Views;
using Kite.Views.Popups;
using OneOf.Types;

namespace Kite.ViewModels
{
    public partial class HomeViewModel : ViewModelBase, INavigateVM, IRecipient<ShutdownMessage>
    {
        const string savePath = "save.json";
        
        readonly FileDialogService fileDialogService;

        [ObservableProperty]
        ObservableCollection<RefrenceFolder> folders = new();

        bool IsEmpty => Folders.Count == 0;

        public HomeViewModel(FileDialogService fileDialogService)
        {
            this.fileDialogService = fileDialogService;

            WeakReferenceMessenger.Default.Register(this);

            LoadFolders();
        }

        void LoadFolders()
        {
            if (File.Exists(savePath))
            {
                var jsonText = File.ReadAllText(savePath);
                Folders = JsonSerializer.Deserialize<ObservableCollection<RefrenceFolder>>(jsonText);

                foreach (var folder in Folders)
                {
                    folder.Init();
                }

                Folders = new(Folders.Distinct());
            }
        }

        public void SelectFolder(object folder) 
        {
            RefrenceFolder refrenceFolder = (RefrenceFolder)folder;

            if (refrenceFolder.Length <= 0) 
            {
                MainView.manager.Show(new Notification("Error", "Folder is empty", NotificationType.Error));
                return;
            }

            var sv = Ioc.Default.GetRequiredService<SeasionConfigView>();
            sv.ViewModel.path = refrenceFolder.Path;
            DialogHost.Show(sv);
        }

        public void Delete(object folder) 
        {
            RefrenceFolder refrenceFolder = (RefrenceFolder)folder;
            Folders.Remove(refrenceFolder);
        }

        public async void AddFolder() 
        {
            var path = await fileDialogService.OpenFolderDialog("Select Folder");
            if (path != null)
            {
                if (Folders.Contains(new(path)) == false)
                    Folders.Add(new(path));
                else
                    MainView.manager.Show(new Notification("Error", "Folder already exist", NotificationType.Error));
            } 
        }

        public void OpenInExplorer(object folder)
        {
            RefrenceFolder refrenceFolder = (RefrenceFolder)folder;
            Helpers.OpenInExplorer(refrenceFolder.Path);
        }

        void IRecipient<ShutdownMessage>.Receive(ShutdownMessage message)
        {
            var jsonText = JsonSerializer.Serialize(Folders);
            File.WriteAllText(savePath, jsonText);
        }

        public void Enter()
        {
            LoadFolders();
        }

        public void Exit()
        {

        }
    }
}
