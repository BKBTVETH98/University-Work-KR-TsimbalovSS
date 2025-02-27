using System.Diagnostics;
using System.Windows.Forms;


namespace KRTsimbalov
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems != null)
            {
                Process.Start(listBox1.SelectedItem.ToString());
            }
        }

        private void Form2_VisibleChanged(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form1"] is Form1 form1)
            {
                foreach (var filePath in form1.filePaths)
                {
                    listBox1.Items.Add(filePath);
                }
            }
        }
    }
}
