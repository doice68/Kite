using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Kite
{
    public static class Helpers
    {
        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action) 
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        public static IEnumerable<string> GetFilesWithExtensions(string path, params string[] extensions) 
        {
            return Directory.GetFiles(path.Replace("%20", " "))
                .Where(file => extensions.Any(file.ToLower().EndsWith));
        }

        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }

        public static void OpenInExplorer(string path) 
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = path
            };
            System.Diagnostics.Process.Start(psi);
        }
    }

    public static class ImageHelper
    {
        public static Bitmap LoadFromResource(string path)
        {
            return new Bitmap(path);
        }

        public static async Task<Bitmap?> LoadFromWeb(Uri url)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsByteArrayAsync();
                return new Bitmap(new MemoryStream(data));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
                return null;
            }
        }
    }
}
