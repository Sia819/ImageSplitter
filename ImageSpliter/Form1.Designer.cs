namespace ImageSpliter
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.OpenImage_Button = new System.Windows.Forms.Button();
            this.ImagePath_TextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Horizontal_SliceSize_TextBox = new System.Windows.Forms.TextBox();
            this.Vertical_SliceSize_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Horizontal_SliceCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Vertical_SliceCount = new System.Windows.Forms.NumericUpDown();
            this.Test_Button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Padding_TextBox = new System.Windows.Forms.TextBox();
            this.Margin_TextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Horizontal_SliceCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vertical_SliceCount)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 260);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(691, 518);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // OpenImage_Button
            // 
            this.OpenImage_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenImage_Button.BackColor = System.Drawing.Color.Transparent;
            this.OpenImage_Button.Location = new System.Drawing.Point(587, 5);
            this.OpenImage_Button.Name = "OpenImage_Button";
            this.OpenImage_Button.Size = new System.Drawing.Size(97, 24);
            this.OpenImage_Button.TabIndex = 1;
            this.OpenImage_Button.Text = "이미지 열기";
            this.OpenImage_Button.UseVisualStyleBackColor = false;
            this.OpenImage_Button.Click += new System.EventHandler(this.OpenImage_Button_Click);
            // 
            // ImagePath_TextBox
            // 
            this.ImagePath_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePath_TextBox.Location = new System.Drawing.Point(6, 6);
            this.ImagePath_TextBox.Name = "ImagePath_TextBox";
            this.ImagePath_TextBox.Size = new System.Drawing.Size(575, 21);
            this.ImagePath_TextBox.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Horizontal_SliceSize_TextBox
            // 
            this.Horizontal_SliceSize_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Horizontal_SliceSize_TextBox.Location = new System.Drawing.Point(274, 78);
            this.Horizontal_SliceSize_TextBox.Multiline = true;
            this.Horizontal_SliceSize_TextBox.Name = "Horizontal_SliceSize_TextBox";
            this.Horizontal_SliceSize_TextBox.Size = new System.Drawing.Size(131, 19);
            this.Horizontal_SliceSize_TextBox.TabIndex = 4;
            // 
            // Vertical_SliceSize_TextBox
            // 
            this.Vertical_SliceSize_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Vertical_SliceSize_TextBox.Location = new System.Drawing.Point(274, 103);
            this.Vertical_SliceSize_TextBox.Multiline = true;
            this.Vertical_SliceSize_TextBox.Name = "Vertical_SliceSize_TextBox";
            this.Vertical_SliceSize_TextBox.Size = new System.Drawing.Size(131, 19);
            this.Vertical_SliceSize_TextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "가로 나눌갯수(Column 개수)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(265, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "가로 나눌 픽셀간격";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Horizontal_SliceCount
            // 
            this.Horizontal_SliceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Horizontal_SliceCount.Location = new System.Drawing.Point(274, 3);
            this.Horizontal_SliceCount.Name = "Horizontal_SliceCount";
            this.Horizontal_SliceCount.Size = new System.Drawing.Size(131, 21);
            this.Horizontal_SliceCount.TabIndex = 9;
            this.Horizontal_SliceCount.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "세로 나눌갯수(Rows 개수)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Vertical_SliceCount
            // 
            this.Vertical_SliceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Vertical_SliceCount.Location = new System.Drawing.Point(274, 28);
            this.Vertical_SliceCount.Name = "Vertical_SliceCount";
            this.Vertical_SliceCount.Size = new System.Drawing.Size(131, 21);
            this.Vertical_SliceCount.TabIndex = 9;
            this.Vertical_SliceCount.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // Test_Button
            // 
            this.Test_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Test_Button.Location = new System.Drawing.Point(589, 189);
            this.Test_Button.Name = "Test_Button";
            this.Test_Button.Size = new System.Drawing.Size(95, 23);
            this.Test_Button.TabIndex = 10;
            this.Test_Button.Text = "테스트";
            this.Test_Button.UseVisualStyleBackColor = true;
            this.Test_Button.Click += new System.EventHandler(this.Test_Button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 103);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "세로 나눌 픽셀간격";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.ImagePath_TextBox);
            this.panel1.Controls.Add(this.Test_Button);
            this.panel1.Controls.Add(this.OpenImage_Button);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 260);
            this.panel1.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(585, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "저장";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Horizontal_SliceCount, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.checkBox1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.Vertical_SliceCount, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.Vertical_SliceSize_TextBox, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.Horizontal_SliceSize_TextBox, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.Padding_TextBox, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.Margin_TextBox, 1, 6);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 36);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2853F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2853F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2853F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2853F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28816F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(408, 178);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.checkBox1, 2);
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox1.Location = new System.Drawing.Point(3, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(402, 19);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "IsAutoSize";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 153);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(265, 22);
            this.label6.TabIndex = 15;
            this.label6.Text = "Internal Margin";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 19);
            this.label5.TabIndex = 16;
            this.label5.Text = "Margin";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Padding_TextBox
            // 
            this.Padding_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Padding_TextBox.Location = new System.Drawing.Point(274, 128);
            this.Padding_TextBox.Multiline = true;
            this.Padding_TextBox.Name = "Padding_TextBox";
            this.Padding_TextBox.Size = new System.Drawing.Size(131, 19);
            this.Padding_TextBox.TabIndex = 13;
            this.Padding_TextBox.TextChanged += new System.EventHandler(this.Padding_TextBox_TextChanged);
            // 
            // Margin_TextBox
            // 
            this.Margin_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Margin_TextBox.Location = new System.Drawing.Point(274, 153);
            this.Margin_TextBox.Multiline = true;
            this.Margin_TextBox.Name = "Margin_TextBox";
            this.Margin_TextBox.Size = new System.Drawing.Size(131, 22);
            this.Margin_TextBox.TabIndex = 13;
            this.Margin_TextBox.TextChanged += new System.EventHandler(this.Margin_TextBox_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(691, 778);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(691, 778);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "윈폼은 똥";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Horizontal_SliceCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vertical_SliceCount)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button OpenImage_Button;
        private System.Windows.Forms.TextBox ImagePath_TextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox Horizontal_SliceSize_TextBox;
        private System.Windows.Forms.TextBox Vertical_SliceSize_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Horizontal_SliceCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Vertical_SliceCount;
        private System.Windows.Forms.Button Test_Button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox Padding_TextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Margin_TextBox;
        private System.Windows.Forms.Button button1;
    }
}

