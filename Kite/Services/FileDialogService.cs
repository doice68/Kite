using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using OneOf;
using OneOf.Types;

namespace Kite.Services
{
    public class FileDialogService
    {
        public async Task<string?> OpenFileDialog(string title) 
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var path = await provider.OpenFilePickerAsync(new() { Title = title });
            return path.Count > 0 ? path[0].Path.AbsolutePath.Replace("%20", " ") : null;
        }

        public async Task<string?> SaveFileDialog(string title) 
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var path = await provider.SaveFilePickerAsync(new() { Title = "title" });
            return path?.Path.AbsolutePath.Replace("%20", " ");
        }

        public async Task<string?> OpenFolderDialog(string title)
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var path = await provider.OpenFolderPickerAsync(new() { Title = "title" });
            return path.Count > 0 ? path[0].Path.AbsolutePath.Replace("%20", " ") : null;
        }
    }
}
