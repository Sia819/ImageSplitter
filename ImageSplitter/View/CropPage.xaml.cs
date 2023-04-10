using ImageSplitter.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageSplitter.View
{
    /// <summary>
    /// CropPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CropPage : Page
    {
        private CropVM ViewModel;
        private bool dragging;
        private System.Windows.Point startPoint;

        public CropPage()
        {
            InitializeComponent();
            if (this.DataContext is CropVM vm) ViewModel = vm;
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.IsCapturing)
            {
                DragLine.Visibility = Visibility.Visible;
                dragging = true;
                startPoint = e.GetPosition(img);

                DragLine.Margin = new Thickness(startPoint.X, startPoint.Y, 0, 0);
                DragLine.Width = 0;
                DragLine.Height = 0;
            }
        }

        private void Image_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.IsCapturing)
            {
                dragging = false;
                ViewModel.IsCapturing = false;

                System.Windows.Point pt = e.GetPosition(img);

                DragLine.Width = pt.X - DragLine.Margin.Left;
                DragLine.Height = pt.Y - DragLine.Margin.Top;
                img.Cursor = Cursors.Arrow;

                int width = ViewModel.OriginalBitmaps[ViewModel.SelectedImage].Width;
                int height = ViewModel.OriginalBitmaps[ViewModel.SelectedImage].Height;

                double scaleX = width / img.ActualWidth;
                double scaleY = height / img.ActualHeight;

                System.Drawing.Rectangle newRect = new System.Drawing.Rectangle()
                {
                    X = (int)(DragLine.Margin.Left * scaleX),
                    Y = (int)(DragLine.Margin.Top * scaleY),
                    Width = (int)(DragLine.Width * scaleX),
                    Height = (int)(DragLine.Height * scaleY),
                };

                if (ViewModel.IsFitMode)
                    newRect = ViewModel.Fit(newRect);

                ViewModel.RectedImages.Add(new Model.ObservableRect(newRect));

                // 사각형 컨트롤을 숨깁니다.
                DragLine.Width = 0; 
                DragLine.Height = 0;
                DragLine.Margin = new Thickness();
                DragLine.Visibility = Visibility.Hidden;
            }
        }

        private void Image_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                System.Windows.Point pt = e.GetPosition(img);
                double xDiff = pt.X - startPoint.X;
                double yDiff = pt.Y - startPoint.Y;

                // Right-Bottom direction
                if (xDiff >= 0 && yDiff >= 0)
                {
                    DragLine.Margin = new Thickness(startPoint.X, startPoint.Y, 0, 0);
                    DragLine.Width = xDiff;
                    DragLine.Height = yDiff;
                }
                // Left-Bottom direction
                else if (xDiff < 0 && yDiff >= 0)
                {
                    DragLine.Margin = new Thickness(pt.X, startPoint.Y, 0, 0);
                    DragLine.Width = -xDiff;
                    DragLine.Height = yDiff;
                }
                // Left-Top direction
                else if (xDiff < 0 && yDiff < 0)
                {
                    DragLine.Margin = new Thickness(pt.X, pt.Y, 0, 0);
                    DragLine.Width = -xDiff;
                    DragLine.Height = -yDiff;
                }
                // Right-Top direction
                else if (xDiff >= 0 && yDiff < 0)
                {
                    DragLine.Margin = new Thickness(startPoint.X, pt.Y, 0, 0);
                    DragLine.Width = xDiff;
                    DragLine.Height = -yDiff;
                }
            }
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ViewModel.IsCapturing)
                img.Cursor = Cursors.Cross;
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            img.Cursor = Cursors.Arrow;
        }
    }
}
