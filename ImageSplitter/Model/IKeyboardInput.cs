using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageSplitter.Model
{
    internal interface IKeyboardInput
    {
        public abstract void KeyDown_Command(KeyEventArgs? e);
    }
}
