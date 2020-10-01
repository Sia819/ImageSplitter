using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageSpliter
{
    public partial class Form1 : Form
    {
        #region BaseBitmap

        private Bitmap _baseBitmap;
        public Bitmap BaseBitmap
        {
            get => _baseBitmap;
            set
            {
                _baseBitmap = value;
                baseBitmap_ImageChanged?.Invoke(this, null);
            }
        }
        private EventHandler baseBitmap_ImageChanged;
        private bool IsBitmapEnable => _baseBitmap != null && _baseBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.DontCare;

        #endregion

        #region Private_Fields

        private int default_penel1_Height = 0;
        private int default_penel1_Width = 0;
        private Thickness padding;
        private Thickness margin;

        #endregion

        #region Form_Constructor

        public Form1()
        {
            InitializeComponent();

            Horizontal_SliceCount.Value = 1;
            Vertical_SliceCount.Value = 1;
            Horizontal_SliceSize_TextBox.Text = "50";
            Vertical_SliceSize_TextBox.Text = "50";
            checkBox1.Checked = true;
            //
            baseBitmap_ImageChanged += baseBitmap_ImageChangedEvent;

            openFileDialog1.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";

            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;

            default_penel1_Height = panel1.Height;
            default_penel1_Width = panel1.Width;
        }

        #endregion

        #region Costom Events

        /// <summary>
        /// 이미지가 변경 된 경우 발생합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseBitmap_ImageChangedEvent(object sender, EventArgs e)
        {
            CalculateAutoSize();
        }

        #endregion

        #region Forms Events

        /// <summary>
        /// numeric Control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown && (int)(((NumericUpDown)sender).Value) <= 0)
                ((NumericUpDown)sender).Value = 1;  // Numeric 1 이하로 내려가지 않습니다.

            CalculateAutoSize();
        }

        /// <summary>
        /// 사진파일을 로드합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenImage_Button_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            ImagePath_TextBox.Text = openFileDialog1.FileName;
            try
            {
                BaseBitmap = new Bitmap(ImagePath_TextBox.Text);  // IO로부터 이미지를 불러옵니다.
            }
            catch
            {
                return;
            }

            PictureBoxImageSetClone(pictureBox1, BaseBitmap); // 이미지를 정리하면서 대입합니다.

            // 윈도우의 크기를 이미지만큼 조절합니다.
            int windowTitleBarSize = this.Height - this.ClientSize.Height;
            this.Height = panel1.Height + BaseBitmap.Height + windowTitleBarSize;

            // TODO : 여기에 옆으로 이미지가 클 경우, 이미가 작을 경우를 구현.

            //if (panel1.Width > pictureBox1.Image.Width)
            //{
            //
            //}
        }

        /// <summary>
        /// 이미지 스플릿을 테스트합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Button_Click(object sender, EventArgs e)
        {
            if (!IsBitmapEnable)
                return;

            PictureBoxImageSetClone(pictureBox1, BaseBitmap); // 이미지를 정리하면서 대입합니다.

            Pen testPen1 = new Pen(Color.Black, 1);
            Pen testPen2 = new Pen(Color.Blue, 1);
            using (var image1 = (Image)BaseBitmap.Clone())
            {
                using (var graphics = Graphics.FromImage(image1))
                {
                    int AxisX_Adder = 0;
                    int AxisY_Adder = 0;
                    for (int i = 0; i < (int)Horizontal_SliceCount.Value; i++)
                    {
                        AxisX_Adder += int.Parse(Horizontal_SliceSize_TextBox.Text);
                        graphics.DrawLine(testPen1, AxisX_Adder, 0, AxisX_Adder, BaseBitmap.Height);
                    }
                    for (int i = 0; i < (int)Vertical_SliceCount.Value; i++)
                    {
                        AxisY_Adder += int.Parse(Vertical_SliceSize_TextBox.Text);
                        graphics.DrawLine(testPen2, 0, AxisY_Adder, BaseBitmap.Width, AxisY_Adder);
                    }
                }
                // **
                List<Rectangle> rects = new List<Rectangle>();
                

                PictureBoxImageSetClone(pictureBox1, image1);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Horizontal_SliceSize_TextBox.ReadOnly =
                Vertical_SliceSize_TextBox.ReadOnly = true;
            }
            else
            {
                Vertical_SliceSize_TextBox.ReadOnly =
                Horizontal_SliceSize_TextBox.ReadOnly = false;
            }
        }

        private void Padding_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                Thickness _padding = TopLeftBottomRight_Split((sender as TextBox).Text);
                if (!(_padding == new Thickness(-1)))
                {
                    (sender as TextBox).Font = new Font((sender as TextBox).Font, FontStyle.Regular);
                    (sender as TextBox).ForeColor = Color.Black;
                    this.padding = _padding;
                }
                else
                {
                    (sender as TextBox).Font = new Font((sender as TextBox).Font, FontStyle.Bold);
                    (sender as TextBox).ForeColor = Color.Red;
                    return;
                }
            }
        }

        private void Margin_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                Thickness _margin = TopLeftBottomRight_Split((sender as TextBox).Text);
                if (!(_margin == new Thickness(-1)))
                {
                    this.margin = _margin;
                }
                else
                {
                    // TODO : sender TextBox에 경고
                    return;
                }
            }
        }


        #endregion Forms Events

        #region Costom Function

        /// <summary>
        /// 기존에 있던 target이미지를 정리하고, 새 이미지의 클론을 대입시킵니다.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="newImage"></param>
        void PictureBoxImageSetClone(PictureBox target, Image newImage)
        {
            if (target.Image != null)
            {
                target.Image.Dispose();
            }
            if (newImage.PixelFormat == System.Drawing.Imaging.PixelFormat.DontCare)
                throw new Exception("null상태의 새 이미지가 패러미터 되었습니다.");

            target.Image = new Bitmap((Image)newImage.Clone());
        }

        /// <summary>
        /// 현재 개수에 맞춰서 계산합니다.
        /// </summary>
        private void CalculateAutoSize()
        {
            if (IsBitmapEnable && checkBox1.Checked)
            {
                Horizontal_SliceSize_TextBox.Text = (BaseBitmap.Width / ((int)Horizontal_SliceCount.Value)).ToString();
                Vertical_SliceSize_TextBox.Text = (BaseBitmap.Height / ((int)Vertical_SliceCount.Value)).ToString();
            }
        }


        /// <summary>
        /// Comma Split to Thickness
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Thickness TopLeftBottomRight_Split(string str)
        {
            str = str.Replace(" ", "");

            string[] _splitted_Str = str.Split(',');
            int[] _margin = new int[_splitted_Str.Length];

            // Parse the integer type from a string array.
            // If it cannot be parsed, it returns an unable margin.
            for (int i = 0; i < _splitted_Str.Length; i++)
                if (!int.TryParse(_splitted_Str[i], out _margin[i]))
                    return new Thickness(-1, -1, -1, -1);

            if (_margin.Length == 4)
            {
                return new Thickness(_margin[0], _margin[1], _margin[2], _margin[3]);
            }
            if (_margin.Length == 2)
            {
                return new Thickness(_margin[0], _margin[1], _margin[0], _margin[1]);
            }
            if (_margin.Length == 1)
            {
                return new Thickness(_margin[0], _margin[0], _margin[0], _margin[0]);
            }
            // Can be parsed integer but, can't support length
            return new Thickness(-1, -1, -1, -1);
        }

        // TODO : 잘못된 값을 입력시 테두리 붉은색 칠하기
        private void WrongInputWarning(Control control)
        {
            MessageBox.Show("잘못된 값을 입력한 디자이너는 아직 구현되지않음.");

        }

        void ControlWarning(Control control)
        {
            //control.
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //BaseBitmap.
        }
    }
}
