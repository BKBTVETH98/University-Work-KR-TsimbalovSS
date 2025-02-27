namespace KRTsimbalov
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            listBox2 = new ListBox();
            label2 = new Label();
            listBox1 = new ListBox();
            label1 = new Label();
            btnSelectFiles = new Button();
            button1 = new Button();
            btnCompareByByte = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources._1486395882_close_80604;
            pictureBox1.Location = new Point(739, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = Properties.Resources._1486395863_menu2_80615;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(32, 32);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // listBox2
            // 
            listBox2.BackColor = Color.FromArgb(75, 69, 79);
            listBox2.BorderStyle = BorderStyle.FixedSingle;
            listBox2.Font = new Font("Segoe UI", 12F);
            listBox2.ForeColor = SystemColors.Window;
            listBox2.FormattingEnabled = true;
            listBox2.HorizontalScrollbar = true;
            listBox2.ItemHeight = 21;
            listBox2.Location = new Point(29, 289);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(600, 149);
            listBox2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(235, 234);
            label2.Name = "label2";
            label2.Size = new Size(188, 28);
            label2.TabIndex = 4;
            label2.Text = "Сравнение файлов";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(75, 69, 79);
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.Font = new Font("Segoe UI", 12F);
            listBox1.ForeColor = SystemColors.Window;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 21;
            listBox1.Location = new Point(29, 57);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(600, 149);
            listBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(237, 4);
            label1.Name = "label1";
            label1.Size = new Size(185, 28);
            label1.TabIndex = 3;
            label1.Text = "Выбранные файлы";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSelectFiles
            // 
            btnSelectFiles.Location = new Point(237, 470);
            btnSelectFiles.Name = "btnSelectFiles";
            btnSelectFiles.Size = new Size(172, 41);
            btnSelectFiles.TabIndex = 0;
            btnSelectFiles.Text = "Выбрать файлы";
            btnSelectFiles.UseVisualStyleBackColor = true;
            btnSelectFiles.Click += btnSelectFiles_Click;
            // 
            // button1
            // 
            button1.Location = new Point(28, 470);
            button1.Name = "button1";
            button1.Size = new Size(172, 41);
            button1.TabIndex = 6;
            button1.Text = "Хеш сравнение";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnCompareByHash_Click;
            // 
            // btnCompareByByte
            // 
            btnCompareByByte.Location = new Point(456, 470);
            btnCompareByByte.Name = "btnCompareByByte";
            btnCompareByByte.Size = new Size(172, 41);
            btnCompareByByte.TabIndex = 7;
            btnCompareByByte.Text = "Побайтовое сравнение";
            btnCompareByByte.UseVisualStyleBackColor = true;
            btnCompareByByte.Click += btnCompareByByte_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCompareByByte);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnSelectFiles);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(listBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(listBox2);
            panel1.Location = new Point(63, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(658, 538);
            panel1.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(55, 69, 69);
            ClientSize = new Size(783, 562);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Opacity = 0.9D;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Идентичность Файлов";
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private ListBox listBox2;
        private Label label2;
        private ListBox listBox1;
        private Label label1;
        private Button btnSelectFiles;
        private Button button1;
        private Button btnCompareByByte;
        private Panel panel1;
    }
}
