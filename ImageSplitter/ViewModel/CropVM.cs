using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageSplitter.Common;
using ImageSplitter.Model;

namespace ImageSplitter.ViewModel
{
    public class CropVM : INotifyPropertyChanged, IKeyboardInput
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Property
        public ObservableCollection<System.Drawing.Image> OriginalBitmaps { get; set; }
        public ObservableCollection<BitmapSource> DisplayImages { get; set; }
        public ObservableCollection<ObservableRect> RectedImages { get; set; }

        /// <summary>
        /// 이미지를 드래그 앤 드롭 한 경우.
        /// </summary>
        public RelayCommand<DragEventArgs> ImageDrop { get; set; }

        /// <summary>
        /// 리스트뷰 안에서 어떤 이미지를 선택하여 보여주고 있는지
        /// </summary>
        public int SelectedImage { get; set; }

        /// <summary>
        /// Crop된 이미지 중 몇번째 이미지를 선택하고 있는지
        /// </summary>
        public int SelectedRectedImage { get; set; }

        public bool IsFitMode { get; set; }

        public bool IsCapturing { get; set; }
        #endregion

        #region Command
        public RelayCommand ImageSelectionChanged { get; set; }
        public RelayCommand AddRectedImage { get; set; }
        public RelayCommand<ListBoxItem> RemoveRectedImage { get; set; }
        public RelayCommand ExportAllImages { get; set; }
        #endregion

        #region Constructor
        public CropVM()
        {
            SelectedImage = -1;

            OriginalBitmaps = new();
            DisplayImages = new();
            RectedImages = new();
            ImageDrop = new(LoadImageFromDragDrop_Command);
            ImageSelectionChanged = new(ImageSelectionChanged_Command);
            AddRectedImage = new(AddRectedImage_Command);
            RemoveRectedImage = new(RemoveRectedImage_Command);
            ExportAllImages = new(ExportAllImages_Command);

            RectedImages.CollectionChanged += (o, e) => Render();
        }
        #endregion

        /// <summary>
        /// IKeyboardInput 구현체
        /// Ctrl + V 동작시 클립보드에 있는 이미지를 프로그램에서 인식합니다.
        /// </summary>
        public void KeyDown_Command(KeyEventArgs? e)
        {
            if (e is null) return;

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsImage())
                {
                    Bitmap img = BitmapExtension.BitmapSourceToBitmap(Clipboard.GetImage());
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
                if (SelectedImage < 0) return;
                if (OriginalBitmaps.Count <= 0) return;

                OriginalBitmaps.RemoveAt(SelectedImage);
                DisplayImages.RemoveAt(SelectedImage);
            }
        }

        #region Command Routine
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
        /// (Ctrl + C) 할 때의 동작코드 입니다.
        /// </summary>
        private void SaveToClipboard_Command()
        {
            if (RectedImages.Count <= 0) return;

            // 마우스로 선택한 잘려진 영역의 이미지를 복사합니다. (여러 이미지 중, 현재 선택중인 이미지에서 Crop 영역이 여러가지가 있습니다. 이 Crop들 중 하나를 선택할 수 있는데, 이 Crop의 선택 인덱스 입니다.)
            var image = new Bitmap(RectedImages[SelectedRectedImage].Width, RectedImages[SelectedRectedImage].Height);
            var graphics = Graphics.FromImage(image);
            graphics.DrawImage(OriginalBitmaps[SelectedImage],  // 여러 이미지 리스트 중에서 현재 선택된 이미지
                               new Rectangle(0, 0, RectedImages[SelectedRectedImage].Width, RectedImages[SelectedRectedImage].Height), // 현재 선택한 Crop 영역으로
                               RectedImages[SelectedRectedImage].ExportRectangle,
                               GraphicsUnit.Pixel);
            graphics.Dispose();
            Clipboard.SetImage(BitmapExtension.BitmapToBitmapImage(image));
        }

        private void AddRectedImage_Command()
        {
            IsCapturing = true;
        }

        private void RemoveRectedImage_Command(ListBoxItem? item)
        {
            if (item is null) throw new Exception("item is null");

            if (item.DataContext is ObservableRect rect)
            {
                RectedImages.Remove(rect);
            }
        }

        private void ImageSelectionChanged_Command()
        {
            Render();
        }

        void ExportAllImages_Command()
        {
            int outputImages = RectedImages.Count * OriginalBitmaps.Count;
            int outputImageCount = 0;
            
            System.Drawing.Image[] images = new System.Drawing.Image[outputImages];

            for(int originImageCount = 0; originImageCount < OriginalBitmaps.Count; originImageCount++) 
            {
                for (int i = 0; i < RectedImages.Count; i++, outputImageCount++)
                {
                    images[outputImageCount] = new Bitmap(RectedImages[i].Width, RectedImages[i].Height);
                    var graphics = Graphics.FromImage(images[outputImageCount]);
                    graphics.DrawImage(OriginalBitmaps[originImageCount],
                                       new Rectangle(0, 0, RectedImages[i].Width, RectedImages[i].Height),
                                       RectedImages[i].ExportRectangle,
                                       GraphicsUnit.Pixel);
                    graphics.Dispose();
                    images[outputImageCount].Save(outputImageCount + ".png", ImageFormat.Png);
                }
            }
            
        }

        private void Render()
        {
            if (SelectedImage < 0) return;
            using Bitmap? temp = OriginalBitmaps[SelectedImage].Clone() as Bitmap;
            if (temp is null) throw new Exception("temp is null");
            using Graphics graphics = Graphics.FromImage(temp);
            var tempSelection = SelectedImage;

            // 잘라질 영역을 그림으로 보여줍니다.
            var rectanglePen = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 255));
            foreach (ObservableRect rect in RectedImages)
            {
                Rectangle rectArea = rect.ExportRectangle;
                graphics.FillRectangle(rectanglePen, rectArea);
            }
            DisplayImages[SelectedImage] = BitmapExtension.BitmapToBitmapImage(temp);
            this.SelectedImage = tempSelection; // 이미지가 교체되면 ListBox의 Selection이 풀리기 때문에 다시 지정해준다.
        }

        // 흰색 배경인지 확인하고 크기를 재조정
        public Rectangle Fit(Rectangle rect)
        {
            using Bitmap? temp = OriginalBitmaps[SelectedImage].Clone() as Bitmap;
            if (temp is null) throw new Exception("temp is null");

            int fitX1 = rect.X;
            int fitY1 = rect.Y;
            int fitX2 = rect.Right;
            int fitY2 = rect.Bottom;
            
            if (IsFitMode == true)
            {
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    for (int y = rect.Top; y < rect.Bottom; y++)
                    {
                        System.Drawing.Color pixelColor = temp.GetPixel(x, y);
                        if (pixelColor.R < 240 || pixelColor.G < 240 || pixelColor.B < 240)
                        {
                            fitX1 = Math.Max(fitX1, x);
                            fitY1 = Math.Max(fitY1, y);
                            fitX2 = Math.Min(fitX2, x);
                            fitY2 = Math.Min(fitY2, y);
                        }
                    }
                }

                rect = new Rectangle()
                {
                    X = fitX2,
                    Y = fitY2,
                    Width = fitX1 - fitX2 + 1,
                    Height = fitY1 - fitY2 + 1
                };
            }

            return rect;
        }
        #endregion

    }
}
