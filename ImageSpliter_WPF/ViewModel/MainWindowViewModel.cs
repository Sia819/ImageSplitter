using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ImageSpliter_WPF.Common;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageSpliter_WPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private Bitmap _originalBitmap;
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



        private EventHandler _originalBitmap_ImageChanged;
        private List<Rectangle> _rectedImages;

        public string Url_TextBox_Text { get; set; }
        public uint SplitColumnsCount_TextBox_Text { get; set; }
        public uint SplitRowsCount_TextBox_Text { get; set; }
        public bool HorizontalSize_CheckBox_IsChecked { get; set; }
        public int HorizontalSize_TextBox_Text { get; set; }
        public bool VerticalSize_CheckBox_IsChecked { get; set; }
        public int VerticalSize_TextBox_Text { get; set; }
        public Thickness OuterMargin_TextBox_Text { get; set; }
        public Thickness InternalMargin_TextBox_Text { get; set; }
        public string DisplayImageMode_ComboBox_Text { get; set; }
        public ObservableCollection<Stretch> DisplayImageMode_ComboBox_ItemsSource { get; set; }
        public BitmapImage DisplayImage_Image_Source { get; set; }
        public ICommand FindImage_Button_Click { get; set; }
        public ICommand DisplayImage_Image_SettingUpdate { get; set; }
        public ICommand Cut_Button_Click { get; set; }

        public MainWindowViewModel()
        {
            // Member & Properties Init // 멤버와 프로퍼티 초기화
            FindImage_Button_Click = new RelayCommand(FindImage_Button_Click_Command);
            DisplayImage_Image_SettingUpdate = new RelayCommand(DisplayImage_Image_SettingUpdate_Command);
            Cut_Button_Click = new RelayCommand(Cut_Button_Click_Command);
            DisplayImageMode_ComboBox_ItemsSource = new ObservableCollection<Stretch>();
            _rectedImages = new List<Rectangle>();
            // Initial Member & Properties variable assignment // 멤버와 프로퍼티의 초기 변수 값 정의

            DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.None);
            DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.Fill);
            DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.Uniform);
            DisplayImageMode_ComboBox_ItemsSource.Add(Stretch.UniformToFill);
            SplitColumnsCount_TextBox_Text = 4;
            SplitRowsCount_TextBox_Text = 8;
            HorizontalSize_CheckBox_IsChecked = true;
            VerticalSize_CheckBox_IsChecked = true;
            InternalMargin_TextBox_Text = new Thickness(10, 10, 10, 10);

        }

        private void FindImage_Button_Click_Command()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            dialog.ShowDialog();
            Url_TextBox_Text = dialog.FileName;
            try
            {
                OriginalBitmap = new Bitmap(dialog.FileName);
            }
            catch
            {
                return;
            }
            DisplayImage_Image_Source = BitmapExtension.BitmapToImageSource(OriginalBitmap);
            // 로딩 된 이미지로 다시 계산합니다.
            DisplayImage_Image_SettingUpdate_Command();
        }

        /// <summary>
        /// 설정을 이미지에 반영합니다.
        /// </summary>
        private void DisplayImage_Image_SettingUpdate_Command()
        {
            // Is Bitmap Loaded
            if (!IsBitmapEnable)
                return;

            List<int> x_Axis = new List<int>();
            List<int> y_Axis = new List<int>();

            RectAnchor calculatedSize = new RectAnchor(0, 0, OriginalBitmap.Width, OriginalBitmap.Height);
            calculatedSize.AnchorX1 += (int)OuterMargin_TextBox_Text.Left;
            calculatedSize.AnchorY1 += (int)OuterMargin_TextBox_Text.Top;
            calculatedSize.AnchorX2 -= (int)OuterMargin_TextBox_Text.Right;
            calculatedSize.AnchorY2 -= (int)OuterMargin_TextBox_Text.Bottom;
            Rectangle rr = calculatedSize.ToRectangle();


            // Calculate Autosize - Horizontal Size & Vertical Size
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

            // Drawline for split debug
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
                        x_Axis.Add(columnSplitAdder);
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
                        y_Axis.Add(rowSplitAdder);
                        rowSplitAdder += VerticalSize_TextBox_Text;
                    }
                    // 사각형 그리기

                    for (int j = 0; j < SplitRowsCount_TextBox_Text; j++)
                    {
                        for (int i = 0; i < SplitColumnsCount_TextBox_Text; i++)
                        {
                            count++;
                            var marginAddedRect = new RectAnchor(x_Axis[i], y_Axis[j], HorizontalSize_TextBox_Text, VerticalSize_TextBox_Text);
                            marginAddedRect.AnchorX1 += (int)InternalMargin_TextBox_Text.Left;
                            marginAddedRect.AnchorY1 += (int)InternalMargin_TextBox_Text.Top;
                            marginAddedRect.AnchorX2 -= (int)InternalMargin_TextBox_Text.Right;
                            marginAddedRect.AnchorY2 -= (int)InternalMargin_TextBox_Text.Bottom;
                            Rectangle rectArea = marginAddedRect.ToRectangle();
                            _rectedImages.Add(rectArea);
                            graphics.FillRectangle(rectanglePen, rectArea);
                            graphics.DrawString(count.ToString(),
                                new Font("맑은 고딕", 12),
                                stringPen,
                                new System.Drawing.Point(x_Axis[i], y_Axis[j]));
                        }
                    }
                }
                // TODO : 모든 잘려질 사각형의 List<Rectangle>를 만들어 위치 저장
                DisplayImage_Image_Source = BitmapExtension.BitmapToImageSource(temp);
            }
        }

        /// <summary>
        /// 이미지를 자르고 저장합니다.
        /// </summary>
        private void Cut_Button_Click_Command()
        {
            Image[] images = new Image[_rectedImages.Count];
            for(int i = 0; i < _rectedImages.Count; i++)
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
    }
}





























/*
 * DisplayImage_Image_SettingUpdate_Command()
 * 완료되면 삭제
int _lef = 0;
int _top = 0;
int _wid = HorizontalSize_TextBox_Text;
int _hei = VerticalSize_TextBox_Text;
int displayCount = 0;

for (int i = 0; i < SplitRowsCount_TextBox_Text; i++)
{
    for (int j = 0; j < SplitColumnsCount_TextBox_Text; j++)
    {
        displayCount++;
        _lef = _lef + (int)InternalMargin_TextBox_Text.Left;
        _top = _top + (int)InternalMargin_TextBox_Text.Top;
        _wid = _wid - (int)(InternalMargin_TextBox_Text.Right) - (int)InternalMargin_TextBox_Text.Left;
        _hei = _hei - (int)(InternalMargin_TextBox_Text.Bottom) - (int)InternalMargin_TextBox_Text.Top;
        _graphics.FillRectangle(_rectanglePen,
            new Rectangle(_lef, _top, _wid, _hei));
        _graphics.DrawString(displayCount.ToString(), new Font("맑은고딕", 12), _stringPen, new System.Drawing.Point(_lef, _top));

        _lef = _lef - (int)InternalMargin_TextBox_Text.Left + HorizontalSize_TextBox_Text;
        _top = _top - (int)InternalMargin_TextBox_Text.Top;
        _wid = _wid + (int)(InternalMargin_TextBox_Text.Right * 2);// + HorizontalSize_TextBox_Text;
        _hei = _hei + (int)(InternalMargin_TextBox_Text.Bottom * 2);
    }
    _lef = 0;   // 줄이 변경될 때 마다 x가 0으로 와야하기 때문에 x1 = 0
    _top = VerticalSize_TextBox_Text * (i + 1);     // 줄이 변경되었을 때 y는 다음줄로 와야하기 때문에
    _wid = HorizontalSize_TextBox_Text;
    _hei = VerticalSize_TextBox_Text;
}
*/