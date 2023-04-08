using CommunityToolkit.Mvvm.Input;
using ImageSplitter.Common;
using ImageSplitter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageSplitter.ViewModel
{
    public class CropVM : INotifyPropertyChanged, IKeyboardInput
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Image> OriginalBitmaps { get; set; } = new ObservableCollection<Image>();
        public ObservableCollection<BitmapSource> DisplayImages { get; set; } = new ObservableCollection<BitmapSource>();

        /// <summary>
        /// 이미지를 드래그 앤 드롭 한 경우.
        /// </summary>
        public RelayCommand<DragEventArgs> ImageDrop { get; set; }

        public int SelectedImage { get; set; }  // 리스트뷰 안에서 어떤 이미지를 선택하여 보여주고 있는지
        public int SelectedRectedImage { get; set; } // Crop된 이미지 중 몇번째 이미지를 선택하고 있는지

        private List<Rectangle> _rectedImages;

        public CropVM()
        {
            this.SelectedImage = -1;
            this.ImageDrop = new RelayCommand<DragEventArgs>(LoadImageFromDragDrop_Command);
        }

        /// <summary>
        /// 드래그 앤 드롭으로 이미지를 가져옵니다.
        /// </summary>
        private void LoadImageFromDragDrop_Command(DragEventArgs? e)
        {
            if (e is null) return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    // 확장자 체크
                    string extension = Path.GetExtension(path);
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                    if (!imageExtensions.Contains(extension.ToLower())) { MessageBox.Show("이미지 확장자 미지원"); return; }

                    try
                    {
                        var img = new Bitmap(path);
                        OriginalBitmaps.Add(img);
                        DisplayImages.Add(BitmapExtension.BitmapToBitmapImage(img));
                    }
                    catch
                    {
                        MessageBox.Show("이미지 파일이 올바르지 않습니다.");
                        return;
                    }
                    this.SelectedImage = DisplayImages.Count - 1;
                }
            }
            else
            {
                MessageBox.Show("이미지 파일만 Drag&Drop 해주세요.");
            }
        }

        /// <summary>
        /// Ctrl + V 동작시 클립보드에 있는 이미지를 프로그램에서 인식합니다.
        /// </summary>
        public void KeyDown_Command(KeyEventArgs? e)
        {
            if (e is null) return;

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsImage())
                {
                    var img = BitmapExtension.BitmapSourceToBitmap(Clipboard.GetImage());
                    this.OriginalBitmaps.Add(img);
                    this.DisplayImages.Add(BitmapExtension.BitmapToBitmapImage(img));
                    this.SelectedImage = DisplayImages.Count - 1;
                }
            }
            else if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                SaveToClipboard_Command();
            }
            else if (e.Key == Key.Delete)
            {
                OriginalBitmaps.RemoveAt(SelectedImage);
                DisplayImages.RemoveAt(SelectedImage);
            }
        }

        private void SaveToClipboard_Command()
        {
            if (_rectedImages.Count <= 0) return;

            // 마우스로 선택한 잘려진 영역의 이미지를 복사합니다. (여러 이미지 중, 현재 선택중인 이미지에서 Crop 영역이 여러가지가 있습니다. 이 Crop들 중 하나를 선택할 수 있는데, 이 Crop의 선택 인덱스 입니다.)
            var image = new Bitmap(_rectedImages[SelectedRectedImage].Width, _rectedImages[SelectedRectedImage].Height);
            var graphics = Graphics.FromImage(image);
            graphics.DrawImage(OriginalBitmaps[SelectedImage],  // 여러 이미지 리스트 중에서 현재 선택된 이미지
                               new Rectangle(0, 0, _rectedImages[SelectedRectedImage].Width, _rectedImages[SelectedRectedImage].Height), // 현재 선택한 Crop 영역으로
                               _rectedImages[SelectedRectedImage],
                               GraphicsUnit.Pixel);
            graphics.Dispose();
            Clipboard.SetImage(BitmapExtension.BitmapToBitmapImage(image));
        }
    }
}
