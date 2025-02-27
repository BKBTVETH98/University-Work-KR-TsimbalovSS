using System.Security.Cryptography;

namespace KRTsimbalov
{
    public partial class Form1 : Form
    {
        private List<string> filePaths = new List<string>();
        private Point mouseOffset;
        private bool isMouseDown = false;
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
                    filePaths = new List<string>(openFileDialog.FileNames);

                    // ��������� ����� ������ � ListBox1
                    listBox1.Items.Clear();
                    foreach (var filePath in filePaths)
                    {
                        listBox1.Items.Add(Path.GetFileName(filePath));
                    }
                }
            }
        }

        // ��������� ������ �� ���� (MD5)
        private void btnCompareByHash_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // ������� ListBox2 ����� ������� ����������

            if (listBox1.Items.Count == 0)
                MessageBox.Show("���������� ������� �����", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Dictionary<string, string> fileHashes = new Dictionary<string, string>();

            // ��������� ���� ������
            foreach (var filePath in filePaths)
            {
                try
                {
                    string hash = GetFileHash(filePath);
                    fileHashes[filePath] = hash;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ ��� ��������� ����� {filePath}: {ex.Message}");
                    continue;
                }
            }

            // ���������� ����� �� ����� � ������� ��������� � ListBox2
            foreach (var file1 in filePaths)
            {
                string identicalFiles = string.Empty;
                foreach (var file2 in filePaths)
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

        // ��������� ������ ���������
        private void btnCompareByByte_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // ������� ListBox2 ����� ������� ����������

            if (listBox1.Items.Count == 0)
                MessageBox.Show("���������� ������� �����", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            // ������� ��� �������� ���������� ���������
            Dictionary<string, List<string>> fileComparisons = new Dictionary<string, List<string>>();

            // ���������� ��� ����� ���������
            for (int i = 0; i < filePaths.Count; i++)
            {
                string file1 = filePaths[i];
                string fileName1 = Path.GetFileName(file1); // �������� ��� ������� �����

                // �������������� ������ ��� �������� ���������� ������
                if (!fileComparisons.ContainsKey(file1))
                {
                    fileComparisons[file1] = new List<string>();
                }

                // ���������� ����1 �� ����� ���������� �������
                for (int j = 0; j < filePaths.Count; j++)
                {
                    // ���������� ��������� ����� � ����� �����
                    if (i == j)
                        continue;

                    string file2 = filePaths[j];
                    string fileName2 = Path.GetFileName(file2); // �������� ��� ������� �����

                    // ���� ����� ��������� ���������
                    if (AreFilesIdentical(file1, file2))
                    {
                        fileComparisons[file1].Add(fileName2); // ��������� ��� ������� ����� � ������ ����������
                    }
                }
            }

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

        // ����� ��� ���������� ���� �����
        private string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        // ����� ��� ����������� ��������� ������
        private bool AreFilesIdentical(string filePath1, string filePath2)
        {
            byte[] file1Bytes = File.ReadAllBytes(filePath1);
            byte[] file2Bytes = File.ReadAllBytes(filePath2);

            if (file1Bytes.Length != file2Bytes.Length)
                return false; // ����� �� ��������� �� �����

            for (int i = 0; i < file1Bytes.Length; i++)
            {
                if (file1Bytes[i] != file2Bytes[i])
                    return false; // ���� ���� ������� � ����������
            }

            return true; // ����� ���������
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

    }
}
