using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using System.Timers;
using OneOf;
using OneOf.Types;
using static System.Net.Mime.MediaTypeNames;

namespace Kite.Services
{
    public class RefrenceSrevice
    {
        public struct Over { }

        string[] pathes;
        public int ItemsCount => pathes.Length;
        int currentPathIndex;

        public bool Init(string dir, bool randomise) 
        {
            pathes = Helpers.GetFilesWithExtensions(dir, ".png", ".jpg").ToArray();
            if (randomise)
                Random.Shared.Shuffle(pathes);
            currentPathIndex = -1;

            return pathes.Length > 0;
        }

        public void StartOver() => 
            currentPathIndex = -1;

        public OneOf<string, Over> GetNext() 
        {
            if (currentPathIndex < pathes.Length - 1) 
            {
                currentPathIndex++;
                refrenceChanged?.Invoke(currentPathIndex);
                return pathes[currentPathIndex];
            }
            
            return new Over();
        }

        public OneOf<string, Over> GetPrevious()
        {
            if (currentPathIndex > 0) 
            {
                currentPathIndex--;
                refrenceChanged?.Invoke(currentPathIndex);
                return pathes[currentPathIndex];
            }
            
            //currentPathIndex = pathes.Length - 1;
            return new Over();
        }

        public Action<int>? refrenceChanged;

        public bool IsFirst() => currentPathIndex == 0;
        public bool IsLast() => currentPathIndex == pathes.Length - 1;

    }
}
