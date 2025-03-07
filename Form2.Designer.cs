namespace KRTsimbalov
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnCompareByByte = new Button();
            button1 = new Button();
            btnSelectFiles = new Button();
            label1 = new Label();
            listBox1 = new ListBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCompareByByte);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnSelectFiles);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(listBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(657, 526);
            panel1.TabIndex = 11;
            // 
            // btnCompareByByte
            // 
            btnCompareByByte.Location = new Point(456, 470);
            btnCompareByByte.Name = "btnCompareByByte";
            btnCompareByByte.Size = new Size(172, 41);
            btnCompareByByte.TabIndex = 7;
            btnCompareByByte.Text = "Открыть папку с файлом";
            btnCompareByByte.UseVisualStyleBackColor = true;
            btnCompareByByte.Click += btnCompareByByte_Click;
            // 
            // button1
            // 
            button1.Location = new Point(28, 470);
            button1.Name = "button1";
            button1.Size = new Size(172, 41);
            button1.TabIndex = 6;
            button1.Text = "Удалить дубли";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnSelectFiles
            // 
            btnSelectFiles.Location = new Point(242, 470);
            btnSelectFiles.Name = "btnSelectFiles";
            btnSelectFiles.Size = new Size(172, 41);
            btnSelectFiles.TabIndex = 0;
            btnSelectFiles.Text = "Открыть";
            btnSelectFiles.UseVisualStyleBackColor = true;
            btnSelectFiles.Click += btnSelectFiles_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(237, 4);
            label1.Name = "label1";
            label1.Size = new Size(177, 28);
            label1.TabIndex = 3;
            label1.Text = "Работа с файлами";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(75, 69, 79);
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.Font = new Font("Segoe UI", 12F);
            listBox1.ForeColor = SystemColors.Window;
            listBox1.FormattingEnabled = true;
            listBox1.HorizontalScrollbar = true;
            listBox1.ItemHeight = 21;
            listBox1.Location = new Point(29, 57);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(600, 380);
            listBox1.TabIndex = 1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(55, 69, 69);
            ClientSize = new Size(658, 538);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            Opacity = 0.85D;
            Text = "MenuForm";
            VisibleChanged += Form2_VisibleChanged;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnCompareByByte;
        private Button button1;
        private Button btnSelectFiles;
        private Label label1;
        private ListBox listBox1;
    }
}