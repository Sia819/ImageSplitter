using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSplitter.Model
{
    public class ModeName
    {
        public string Name { get; set; }
        public ImageSplitMode Mode { get; set; }
        public ModeName(string name, ImageSplitMode mode)
        {
            Name = name;
            Mode = mode;
        }
    }
}
