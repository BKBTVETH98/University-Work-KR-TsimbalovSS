using System.Security.Cryptography;

namespace KRTsimbalov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OpForm(); //вызов метода 

        }
        async void OpForm() //плавное появление формы
        {
            for (Opacity = 0; Opacity <= .85; Opacity += .1)
            {
                await Task.Delay(25);
            }
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            // Открываем диалог для выбора файлов
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true; // Разрешаем выбор нескольких файлов

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

            // Очистить ListBox1 и ListBox2 перед добавлением новых файлов и результатов
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            // Добавляем имена файлов в ListBox1
            foreach (var filePath in filePaths)
            {
                listBox1.Items.Add(Path.GetFileName(filePath));
            }

            // Вычисляем хеши файлов
            foreach (var filePath in filePaths)
            {
                try
                {
                    string hash = GetFileHash(filePath);
                    fileHashes[filePath] = hash;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обработке файла {filePath}: {ex.Message}");
                    continue;
                }
            }

            // Сравниваем файлы по хешам
            foreach (var file1 in filePaths)
            {
                string identicalFiles = string.Empty;
                foreach (var file2 in filePaths)
                {
                    if (file1 != file2 && fileHashes[file1] == fileHashes[file2])
                    {
                        if (string.IsNullOrEmpty(identicalFiles))
                            identicalFiles = Path.GetFileName(file2); // Используем только имя файла
                        else
                            identicalFiles += ", " + Path.GetFileName(file2); // Добавляем только имя файла
                    }
                }

                if (string.IsNullOrEmpty(identicalFiles))
                {
                    fileComparisonResult[file1] = $"Отличается от всех выбранных файлов.";
                }
                else
                {
                    fileComparisonResult[file1] = $"Идентичен файлам -- {identicalFiles}";
                }
            }

            // Добавляем результаты в ListBox2
            foreach (var result in fileComparisonResult)
            {
                listBox2.Items.Add($"{Path.GetFileName(result.Key)} -- {result.Value}"); // Используем только имя файла
            }
        }

        private string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())  // Можно использовать SHA256 для большей безопасности
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
