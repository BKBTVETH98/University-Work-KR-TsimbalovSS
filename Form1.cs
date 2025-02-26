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

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            // ��������� ������ ��� ������ ������
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true; // ��������� ����� ���������� ������

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<string> filePaths = new List<string>(openFileDialog.FileNames);
                    CompareFiles(filePaths);
                }
            }
        }

        private void CompareFiles(List<string> filePaths)
        {
            Dictionary<string, string> fileHashes = new Dictionary<string, string>();
            Dictionary<string, string> fileComparisonResult = new Dictionary<string, string>();

            // �������� ListBox1 � ListBox2 ����� ����������� ����� ������ � �����������
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            // ��������� ����� ������ � ListBox1
            foreach (var filePath in filePaths)
            {
                listBox1.Items.Add(Path.GetFileName(filePath));
            }

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

            // ���������� ����� �� �����
            foreach (var file1 in filePaths)
            {
                string identicalFiles = string.Empty;
                foreach (var file2 in filePaths)
                {
                    if (file1 != file2 && fileHashes[file1] == fileHashes[file2])
                    {
                        if (string.IsNullOrEmpty(identicalFiles))
                            identicalFiles = Path.GetFileName(file2); // ���������� ������ ��� �����
                        else
                            identicalFiles += ", " + Path.GetFileName(file2); // ��������� ������ ��� �����
                    }
                }

                if (string.IsNullOrEmpty(identicalFiles))
                {
                    fileComparisonResult[file1] = $"���������� �� ���� ��������� ������.";
                }
                else
                {
                    fileComparisonResult[file1] = $"��������� ������ -- {identicalFiles}";
                }
            }

            // ��������� ���������� � ListBox2
            foreach (var result in fileComparisonResult)
            {
                listBox2.Items.Add($"{Path.GetFileName(result.Key)} -- {result.Value}"); // ���������� ������ ��� �����
            }
        }

        private string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())  // ����� ������������ SHA256 ��� ������� ������������
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
