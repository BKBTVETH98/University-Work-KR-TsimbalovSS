using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace KRTsimbalov
{
    public partial class Form1 : Form
    {
        private Form currentForm;
        private Point mouseOffset;
        private bool isMouseDown = false;
        private List<Control> savedControls = new List<Control>();
        private Dictionary<string, List<string>> fileComparisons = new Dictionary<string, List<string>>();
        public Form2 form2 = new Form2();


        public Form1()
        {
            InitializeComponent();
            OpForm(); //����� ������ 

        }
        async void OpForm() //������� ��������� �����
        {
            for (Opacity = 0; Opacity <= .85; Opacity += .1)
            {
                await Task.Delay(25);
            }
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true; // ��������� ����� ���������� ������

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Algoritm.filePaths = new List<string>(openFileDialog.FileNames);

                    // ��������� ����� ������ � ListBox1
                    listBox1.Items.Clear();
                    foreach (var filePath in Algoritm.filePaths)
                    {
                        listBox1.Items.Add(Path.GetFileName(filePath));
                    }
                }
            }
        }

        // ��������� ������ �� ���� (MD5)
        private void btnCompareByHash_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count == 0)
                MessageBox.Show("���������� ������� �����", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Dictionary<string, string> fileHashes = new Dictionary<string, string>();
            listBox2.Items.Clear();
            try
            {


                // ��������� ���� ������
                foreach (var filePath in Algoritm.filePaths)
                {

                    string hash = Algoritm.GetFileHash(filePath);
                    fileHashes[filePath] = hash;
                    continue;
                }

                // ���������� ����� �� ����� � ������� ��������� � ListBox2
                foreach (var file1 in Algoritm.filePaths)
                {
                    string identicalFiles = string.Empty;
                        foreach (var file2 in Algoritm.filePaths)
                        {
                            if (file1 != file2 && fileHashes[file1] == fileHashes[file2])
                            {
                                if (string.IsNullOrEmpty(identicalFiles))
                                    identicalFiles = Path.GetFileName(file2);
                                else
                                    identicalFiles += ", " + Path.GetFileName(file2);
                            }
                        }


                    if (string.IsNullOrEmpty(identicalFiles))
                    {
                        listBox2.Items.Add($"{Path.GetFileName(file1)}: ���������� �� ���� ��������� ������.");
                    }
                    else
                    {
                        listBox2.Items.Add($"{Path.GetFileName(file1)}: ��������� ������: {identicalFiles}");
                    }
                }
            }
            catch (Exception ex)
            {
                Algoritm.CleanData(listBox1,listBox2, Algoritm.filePaths, fileComparisons);
            }
        }

        // ��������� ������ ���������
        private void btnCompareByByte_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count == 0)
                MessageBox.Show("���������� ������� �����", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            listBox2.Items.Clear();
            try
            {
                for (int i = 0; i < Algoritm.filePaths.Count; i++)
                {
                    string file1 = Algoritm.filePaths[i];
                    string fileName1 = Path.GetFileName(file1); // �������� ��� ������� �����

                    // �������������� ������ ��� �������� ���������� ������
                    if (!fileComparisons.ContainsKey(file1))
                    {
                        fileComparisons[file1] = new List<string>();
                    }

                    // ���������� ����1 �� ����� ���������� �������
                    for (int j = 0; j < Algoritm.filePaths.Count; j++)
                    {
                        // ���������� ��������� ����� � ����� �����
                        if (i == j)
                            continue;

                        string file2 = Algoritm.filePaths[j];
                        string fileName2 = Path.GetFileName(file2); // �������� ��� ������� �����

                        // ���� ����� ��������� ���������
                        if (Algoritm.AreFilesIdentical(file1, file2))
                        {
                            fileComparisons[file1].Add(fileName2); // ��������� ��� ������� ����� � ������ ����������
                        }
                    }
                }
                // ���������� ��� ����� ���������

                // ������� ���������� � ListBox2
                foreach (var fileComparison in fileComparisons)
                {
                    string fileName1 = Path.GetFileName(fileComparison.Key);
                    string identicalFiles = string.Join(", ", fileComparison.Value); // ����������� ������ � ������ ����� �������

                    if (string.IsNullOrEmpty(identicalFiles))
                    {
                        // ���� �� ������� ���������� ������
                        listBox2.Items.Add($"{fileName1}: ���������� �� ���� ��������� ������.");
                    }
                    else
                    {
                        // ���� ����� ��������� ���� �� � �����
                        listBox2.Items.Add($"{fileName1}: ��������� ������: {identicalFiles}");
                    }
                    // ������ ��� ����������� ���������
                }
            }
            catch
            {
                Algoritm.CleanData(listBox1, listBox2, Algoritm.filePaths, fileComparisons);
            }   
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // ����������, ��� ���� ������, � ����� ������������ ������ �������� ���� �����
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = new Point(e.X, e.Y);
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                // ������ ������ ��������� �����
                Point newLocation = this.Location;
                newLocation.X += e.X - mouseOffset.X;
                newLocation.Y += e.Y - mouseOffset.Y;
                this.Location = newLocation;
            }
        }

        // ������� ��� ���������� ��������������
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false; // ������������� ��������������
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenChildForm(form2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form2 = new Form2();
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentForm != null)
            {
                // ���������� ���������� ��������
                panel1.Controls.Clear();
                panel1.Controls.AddRange(savedControls.ToArray());
                currentForm.Hide();
                currentForm = null;
                savedControls.Clear();
            }
            else
            {

                savedControls = panel1.Controls.Cast<Control>().ToList();
                currentForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panel1.Controls.Add(childForm);
                panel1.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();

            }

        }
    }
}