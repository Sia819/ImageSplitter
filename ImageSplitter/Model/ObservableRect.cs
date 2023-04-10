using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace ImageSplitter.Model
{
    public class ObservableRect : ObservableObject
    {
        System.Drawing.Rectangle rectangle;

        private int x;
        private int y; 
        private int width; 
        private int height;

        #region Public Field
        public int X
        {
            get => rectangle.X;
            set
            {
                SetProperty(ref x, value);
                rectangle.X = value;
            }
        }
        public int Y
        {
            get => rectangle.Y;
            set
            {
                SetProperty(ref y, value);
                rectangle.Y = value;
            }
        }
        public int Width
        {
            get => rectangle.Width;
            set
            { 
                SetProperty(ref width, value);
                rectangle.Width = value;
            }
        }
        public int Height
        {
            get => rectangle.Height;
            set
            {
                SetProperty(ref height, value);
                rectangle.Height = value;
            }
        }

        public int Left => rectangle.X;
        public int Top => rectangle.Y;
        public int Right => rectangle.Right;
        public int Bottom => rectangle.Bottom;
        public bool IsEmpty => rectangle.IsEmpty;

        public System.Drawing.Rectangle ExportRectangle => rectangle;

        #endregion

        public ObservableRect()
        {
            rectangle = new System.Drawing.Rectangle();
        }

        public ObservableRect(int x, int y, int width, int height)
        {
            rectangle = new System.Drawing.Rectangle(x, y, width, height);
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public ObservableRect(System.Drawing.Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.Width = rectangle.Width;
            this.Height = rectangle.Height;
        }
    }
}
