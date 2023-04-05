using ImageSpliter.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;
using System.Linq;
using System.Drawing;
using CommunityToolkit.Mvvm.Input;
using System.Drawing.Imaging;
using ImageSpliter.Model;

using System.Windows.Input;

namespace ImageSpliter.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Bitmap OriginalBitmap
        {
            get => _originalBitmap;
            set
            {
                _originalBitmap = value;
                _originalBitmap_ImageChanged?.Invoke(this, null);
            }
        }
        public bool IsBitmapEnable => _originalBitmap != null && _originalBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.DontCare;


        #region Public Properties
        public double GridModeHeight { get; set; }
        public double WhiteSpaceRemoveModeHeight { get; set; }

        /// <summary>
        /// 불러와진 이미지 주소
        /// </summary>
        public string Uri_TextBox_Text { get; set; }

        /// <summary>
        /// 선택된 자르기 모드
        /// </summary>
        public int SplitMode_SelectedIndex { get; set; }

        /// <summary>
        /// 세로로 자를 개수
        /// </summary>
        public uint SplitColumnsCount_TextBox_Text { get; set; }

        /// <summary>
        /// 가로로 자를 개수
        /// </summary>
        public uint SplitRowsCount_TextBox_Text { get; set; }

        /// <summary>
        /// 너비 자동 계산 활성화
        /// </summary>
        public bool HorizontalSize_CheckBox_IsChecked { get; set; }

        /// <summary>
        /// 잘라질 단위당 너비 값
        /// </summary>
        public int HorizontalSize_TextBox_Text { get; set; }

        /// <summary>
        /// 높이 자동 계산 활성화
        /// </summary>
        public bool VerticalSize_CheckBox_IsChecked { get; set; }

        /// <summary>
        /// 잘라질 단위당 높이 계산값
        /// </summary>
        public int VerticalSize_TextBox_Text { get; set; }

        /// <summary>
        /// 바깥쪽 마진 값
        /// </summary>
        public Thickness OuterMargin_TextBox_Text { get; set; }

        /// <summary>
        /// 단위요소 마진 값
        /// </summary>
        public Thickness InternalMargin_TextBox_Text { get; set; }

        /// <summary>
        /// 표시될 이미지 모드 텍스트
        /// </summary>
        public string DisplayImageMode_ComboBox_Text { get; set; }
        /// <summary>
        /// 이미지 스플릿 모드
        /// </summary>
        public ObservableCollection<ImageSplitMode> ImageSplitMode_ComboBox_ItemsSource { get; set; }

        /// <summary> 
        /// 현재 이미지의 표시 모드 변경 리스트 
        /// </summary>
        public ObservableCollection<Stretch> DisplayImageMode_ComboBox_ItemsSource { get; set; }

        /// <summary>
        /// Image 컨트롤에 표시될 이미지
        /// </summary>
        public BitmapImage DisplayImage_Image_Source { get; set; }
        #endregion Public Properties

        
        #region Commands
        /// <summary>
        /// 이미지 불러오기 버튼 커맨드
        /// </summary>
        public RelayCommand FindImage_Button_Click { get; set; }

        /// <summary>
        /// 설정값이 변경되었으므로 이미지에 표시될 값을 다시 계산합니다.
        /// </summary>
        public RelayCommand DisplayImage_Image_SettingUpdate { get; set; }

        /// <summary>
        /// 이미지를 자르고 저장합니다.
        /// </summary>
        public RelayCommand Cut_Button_Click { get; set; }

        /// <summary>
        /// 이미지를 드래그 앤 드롭 한 경우.
        /// </summary>
        public RelayCommand<DragEventArgs> ImageDrop { get; set; }

        /// <summary>
        /// 자르기 모드가 변경된 경우
        /// </summary>
        public RelayCommand SplitModeChanged { get; set; }

        /// <summary>
        /// 이미지를 클립보드로 저장합니다.
        /// </summary>
        public RelayCommand CopyClipBoard { get; set; }

        public RelayCommand<KeyEventArgs> Window_KeyDown { get; set; }
        #endregion Commands


        #region private fields
        private EventHandler _originalBitmap_ImageChanged;
        private List<Rectangle> _rectedImages;
        private Bitmap _originalBitmap; // property 1:1
        private ImageSplitMode _currentSplitMode;
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            // Command 바인딩 리스너 등록
            this.FindImage_Button_Click = new RelayCommand(LoadImageFromDialog_Command);
            this.DisplayImage_Image_SettingUpdate = new RelayCommand(DisplayImage_Image_SettingUpdate_Command);
            this.Cut_Button_Click = new RelayCommand(Cut_Button_Click_Command);
            this.ImageDrop = new RelayCommand<DragEventArgs>(LoadImageFromDragDrop_Command);
            this.SplitModeChanged = new RelayCommand(SplitModeChanged_Command);
            this.CopyClipBoard = new RelayCommand(SaveToClipboard_Command);
            this.Window_KeyDown = new RelayCommand<KeyEventArgs>(Window_KeyDown_Command);

            this.ImageSplitMode_ComboBox_ItemsSource = new ObservableCollection<ImageSplitMode>();
            this.ImageSplitMode_ComboBox_ItemsSource.Add(ImageSplitMode.GridMode);
            this.ImageSplitMode_ComboBox_ItemsSource.Add(ImageSplitMode.WhiteSpaceRemoveMode);

            this.DisplayImageMode_ComboBox_ItemsSource = new ObservableCollection<Stretch>();
            this.DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.None);
            this.DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.Fill);
            this.DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.Uniform);
            this.DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.UniformToFill);

            // GridMode 잘려질 좌표값들 리스트
            this._rectedImages = new List<Rectangle>();

            this.SplitMode_SelectedIndex = 0;
            this.GridModeHeight = double.NaN;
            this._currentSplitMode = ImageSplitMode.GridMode;

            // TextBox 초기값
            this.SplitColumnsCount_TextBox_Text = 4;
            this.SplitRowsCount_TextBox_Text = 8;
            this.HorizontalSize_CheckBox_IsChecked = true;
            this.VerticalSize_CheckBox_IsChecked = true;
            this.InternalMargin_TextBox_Text = new Thickness(10, 10, 10, 10);
        }
        #endregion Constructor

        #region Private Command
        /// <summary>
        /// 자르기 모드가 변경되었습니다.
        /// </summary>
        private void SplitModeChanged_Command()
        {
            switch (SplitMode_SelectedIndex)
            {
                case 0:
                    _currentSplitMode = ImageSplitMode.GridMode;
                    GridModeHeight = double.NaN;
                    WhiteSpaceRemoveModeHeight = 0;
                    break;
                case 1:
                    _currentSplitMode = ImageSplitMode.WhiteSpaceRemoveMode;
                    GridModeHeight = 0;
                    WhiteSpaceRemoveModeHeight = double.NaN;
                    break;
                default:
                    break;
            }
            DisplayImage_Image_SettingUpdate_Command();
        }

        /// <summary>
        /// 다이얼로그를 통해 이미지를 가져옵니다.
        /// </summary>
        private void LoadImageFromDialog_Command()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            dialog.ShowDialog();
            Uri_TextBox_Text = dialog.FileName;
            try
            {
                OriginalBitmap = new Bitmap(dialog.FileName);
            }
            catch
            {
                return;
            }
            DisplayImage_Image_Source = BitmapExtension.BitmapToBitmapImage(OriginalBitmap);
            // 로딩 된 이미지로 다시 계산합니다.
            DisplayImage_Image_SettingUpdate_Command();
        }

        /// <summary>
        /// 드래그 앤 드롭으로 이미지를 가져옵니다.
        /// </summary>
        /// <param name="e"></param>
        private void LoadImageFromDragDrop_Command(DragEventArgs? e)
        {
            if (e!.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                // 확장자 체크
                string extension = Path.GetExtension(path);
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                if (!imageExtensions.Contains(extension.ToLower())) { MessageBox.Show("이미지 확장자 미지원"); return; }

                try
                {
                    OriginalBitmap = new Bitmap(path);
                    Uri_TextBox_Text = path;
                }
                catch
                {
                    MessageBox.Show("이미지 파일이 올바르지 않습니다.");
                    return;
                }
                DisplayImage_Image_Source = BitmapExtension.BitmapToBitmapImage(OriginalBitmap);
                DisplayImage_Image_SettingUpdate_Command();
            }
            else
            {
                MessageBox.Show("이미지 파일만 Drag&Drop 해주세요.");
            }
        }

        /// <summary>
        /// 설정을 이미지에 반영합니다.
        /// </summary>
        private void DisplayImage_Image_SettingUpdate_Command()
        {
            // Is Bitmap Loaded
            if (!IsBitmapEnable) return;

            if (_currentSplitMode == ImageSplitMode.GridMode)
                GridModeImageSplit();
            else if (_currentSplitMode == ImageSplitMode.WhiteSpaceRemoveMode)
                WhiteSpaceRemoveMode();

        }

        /// <summary>
        /// 이미지를 자르고 저장합니다.
        /// </summary>
        private void Cut_Button_Click_Command()
        {
            Image[] images = new Image[_rectedImages.Count];
            for (int i = 0; i < _rectedImages.Count; i++)
            {
                images[i] = new Bitmap(_rectedImages[i].Width, _rectedImages[i].Height);
                var graphics = Graphics.FromImage(images[i]);
                graphics.DrawImage(OriginalBitmap,
                    new Rectangle(0, 0, _rectedImages[i].Width, _rectedImages[i].Height),
                    _rectedImages[i],
                    GraphicsUnit.Pixel);
                graphics.Dispose();
                images[i].Save(i + ".png", ImageFormat.Png);
            }
        }

        
        private void GridModeImageSplit()
        {
            List<int> x_AxisSplit = new List<int>();        // x축 잘라질 좌표값
            List<int> y_AxisSplit = new List<int>();        // y축 잘라질 좌표값

            RectAnchor calculatedSize = new RectAnchor(0, 0, OriginalBitmap.Width, OriginalBitmap.Height);  // 원본 비트맵의 크기로부터 사각형 범위를 지정합니다.

            calculatedSize.AnchorX1 += (int)OuterMargin_TextBox_Text.Left;      // 바깥 Margin만큼 제거 연산을 수행합니다.
            calculatedSize.AnchorY1 += (int)OuterMargin_TextBox_Text.Top;       //
            calculatedSize.AnchorX2 -= (int)OuterMargin_TextBox_Text.Right;     //
            calculatedSize.AnchorY2 -= (int)OuterMargin_TextBox_Text.Bottom;    //

            // AutoSize 옵션으로, 전체 너비&높이에서 스플릿 개수만큼 나누어 스플릿 사이즈를 계산합니다.
            if (HorizontalSize_CheckBox_IsChecked)
            {
                if (SplitColumnsCount_TextBox_Text > 0)
                    HorizontalSize_TextBox_Text = (int)(calculatedSize.Width / (SplitColumnsCount_TextBox_Text));
            }
            if (VerticalSize_CheckBox_IsChecked)
            {
                if (SplitRowsCount_TextBox_Text > 0)
                    VerticalSize_TextBox_Text = (int)(calculatedSize.Height / (SplitRowsCount_TextBox_Text));
            }

            // 잘라질 영역을 그림으로 보여줍니다.
            var columnPen = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
            var rowPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1);
            var rectanglePen = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 255));
            var stringPen = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            using (Bitmap temp = OriginalBitmap.Clone() as Bitmap)
            {
                using (Graphics graphics = Graphics.FromImage(temp))
                {
                    int columnSplitAdder = calculatedSize.AnchorX1;
                    int rowSplitAdder = calculatedSize.AnchorY1;
                    int count = 0;
                    _rectedImages.Clear();

                    // 세로 선 긋기
                    for (int i = 0; i < SplitColumnsCount_TextBox_Text + 1; i++)
                    {
                        graphics.DrawLine(columnPen,
                            columnSplitAdder,
                            calculatedSize.AnchorY1,
                            columnSplitAdder,
                            calculatedSize.AnchorY2);
                        x_AxisSplit.Add(columnSplitAdder);
                        columnSplitAdder += HorizontalSize_TextBox_Text;
                    }

                    // 가로 선 긋기
                    for (int i = 0; i < SplitRowsCount_TextBox_Text + 1; i++)
                    {
                        graphics.DrawLine(rowPen,
                            calculatedSize.AnchorX1,
                            rowSplitAdder,
                            calculatedSize.AnchorX2,
                            rowSplitAdder);
                        y_AxisSplit.Add(rowSplitAdder);
                        rowSplitAdder += VerticalSize_TextBox_Text;
                    }

                    // 사각형 그리기
                    for (int j = 0; j < SplitRowsCount_TextBox_Text; j++)
                    {
                        for (int i = 0; i < SplitColumnsCount_TextBox_Text; i++)
                        {
                            count++;
                            var marginAddedRect = new RectAnchor(x_AxisSplit[i], y_AxisSplit[j], HorizontalSize_TextBox_Text, VerticalSize_TextBox_Text);
                            marginAddedRect.AnchorX1 += (int)InternalMargin_TextBox_Text.Left;
                            marginAddedRect.AnchorY1 += (int)InternalMargin_TextBox_Text.Top;
                            marginAddedRect.AnchorX2 -= (int)InternalMargin_TextBox_Text.Right;
                            marginAddedRect.AnchorY2 -= (int)InternalMargin_TextBox_Text.Bottom;
                            Rectangle rectArea = marginAddedRect.ToRectangle();
                            _rectedImages.Add(rectArea);
                            graphics.FillRectangle(rectanglePen, rectArea);
                            graphics.DrawString(count.ToString(),
                                new Font("맑은 고딕", 14),
                                stringPen,
                                new System.Drawing.Point(x_AxisSplit[i], y_AxisSplit[j]));
                        }
                    }
                }
                // TODO : 모든 잘려질 사각형의 List<Rectangle>를 만들어 위치 저장
                DisplayImage_Image_Source = BitmapExtension.BitmapToBitmapImage(temp);
            }
        }

        private void WhiteSpaceRemoveMode()
        {
            using (Bitmap temp = OriginalBitmap.Clone() as Bitmap)
            {
                _rectedImages.Clear();

                // Find the boundaries of the non-white region
                int left = 0;
                int top = 0;
                int right = OriginalBitmap.Width - 1;
                int bottom = OriginalBitmap.Height - 1;
                

                for (int x = 0; x < OriginalBitmap.Width; x++)
                {
                    for (int y = 0; y < OriginalBitmap.Height; y++)
                    {
                        System.Drawing.Color pixelColor = OriginalBitmap.GetPixel(x, y);
                        if (pixelColor.R < 240 || pixelColor.G < 240 || pixelColor.B < 240)
                        {
                            left = Math.Max(left, x);
                            top = Math.Max(top, y);
                            right = Math.Min(right, x);
                            bottom = Math.Min(bottom, y);
                        }
                    }
                }

                // Create a new image with the dimensions of the cropped region
                int newWidth = left - right + 1;
                int newHeight = top - bottom + 1;

                // 잘라질 영역을 그림으로 보여줍니다.
                var rectanglePen = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 255));

                using (Graphics graphics = Graphics.FromImage(temp))
                {
                    RectAnchor rectAnchor = new RectAnchor(right, bottom, newWidth, newHeight);
                    Rectangle rectArea = rectAnchor.ToRectangle();
                    _rectedImages.Add(rectArea);
                    graphics.FillRectangle(rectanglePen, rectArea);
                }

                DisplayImage_Image_Source = BitmapExtension.BitmapToBitmapImage(temp);

            }
        }

        private void SaveToClipboard_Command()
        {
            var image = new Bitmap(_rectedImages[0].Width, _rectedImages[0].Height);
            var graphics = Graphics.FromImage(image);
            graphics.DrawImage(OriginalBitmap,
                               new Rectangle(0, 0, _rectedImages[0].Width, _rectedImages[0].Height), 
                               _rectedImages[0], 
                               GraphicsUnit.Pixel);
            graphics.Dispose();
            Clipboard.SetImage(BitmapExtension.BitmapToBitmapImage(image));
        }

        /// <summary>
        /// Ctrl + V 동작시 클립보드에 있는 이미지를 프로그램에서 인식합니다.
        /// </summary>
        private void Window_KeyDown_Command(KeyEventArgs? e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsImage())
                {
                    this.OriginalBitmap = BitmapExtension.BitmapSourceToBitmap(Clipboard.GetImage());
                    DisplayImage_Image_SettingUpdate_Command();
                }
            }
            else if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                SaveToClipboard_Command();
            }
        }
        #endregion

    }
}
