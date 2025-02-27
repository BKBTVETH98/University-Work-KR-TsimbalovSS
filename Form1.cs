using System.Security.Cryptography;

namespace KRTsimbalov
{
    public partial class Form1 : Form
    {
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

        private List<string> filePaths = new List<string>();
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
                bool isIdentical = false;
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

            // ���������� ����� ��������� � ������� ��������� � ListBox2
            for (int i = 0; i < filePaths.Count; i++)
            {
                for (int j = i + 1; j < filePaths.Count; j++)
                {
                    string file1 = filePaths[i];
                    string file2 = filePaths[j];

                    if (AreFilesIdentical(file1, file2))
                    {
                        listBox2.Items.Add($"{Path.GetFileName(file1)} � {Path.GetFileName(file2)}: ���������.");
                    }
                    else
                    {
                        listBox2.Items.Add($"{Path.GetFileName(file1)} � {Path.GetFileName(file2)}: ����������.");
                    }
                }
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
    }
}
