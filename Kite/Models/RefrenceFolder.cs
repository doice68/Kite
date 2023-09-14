using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;

namespace Kite.Models
{
    public record RefrenceFolder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Length { get; set; }

        public bool Empty => Length > 0;

        public RefrenceFolder(string path)
        {
            this.Path = path;
            this.Name = path.Substring(path.LastIndexOf('/') + 1);
            Init();
        }

        public void Init() 
        {
            Length = Helpers.GetFilesWithExtensions(Path, ".png", ".jpg").Count();
        }
    }
}
