using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.ComponentModel;
using System.Drawing;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(TextBoxEx_Paint);
            this.Resize += new EventHandler(TextBoxEx_Resize);
            textBox1.Multiline = true;
            textBox1.BorderStyle = BorderStyle.None;
            this.Controls.Add(textBox1);

            InvalidateSize();
        }


        private Color borderColor = Color.Gray;

        public event EventHandler TextBoxClick
        {
            add { textBox1.Click += value; }
            remove { textBox1.Click -= value; }
        }

        private void TextBoxEx_Resize(object sender, EventArgs e)
        {
            InvalidateSize();
        }
        private void TextBoxEx_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, borderColor, ButtonBorderStyle.Solid);
        }

        private void InvalidateSize()
        {
            textBox1.Size = new Size(this.Width - 2, this.Height - 2);
            textBox1.Location = new Point(1, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
}