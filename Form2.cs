using KRTsimbalov.Class;
using System.Diagnostics;


namespace KRTsimbalov
{
    public partial class Form2 : Form
    {   // Словарь для хранения уникальных хешей
        Dictionary<string, string> uniqueFiles = new Dictionary<string, string>();

        // Проходим по всем элементам ListBox и оставляем только уникальные
        List<string> filesToRemove = new List<string>();
        public Form2()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (listBox1.SelectedItem.ToString().Contains(".txt"))
                    Process.Start("notepad.exe", listBox1.SelectedItem.ToString());
                else
                    try
                    {
                        Process.Start(listBox1.SelectedItem.ToString());
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"{ex}","Ошибка");
                    }
            }
        }

        private void Form2_VisibleChanged(object sender, EventArgs e)
        {
            if (Algoritm.filePaths.Count != 0 && listBox1.Items.Count == 0)
            {
                foreach (var filePath in Algoritm.filePaths)
                {
                    listBox1.Items.Add(filePath);
                }
            }
            else
            {
                listBox1.Items.Clear();
            }
        }

        private void btnCompareByByte_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Process.Start("explorer.exe", $"/select,\"{listBox1.SelectedItem.ToString()}\"");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox1.Items)
            {
                string filePath = item.ToString(); // Получаем полный путь файла
                string fileHash = Algoritm.GetFileHash(filePath); // Получаем хеш файла

                // Если файл с таким хешем уже существует, помечаем его для удаления
                if (uniqueFiles.ContainsValue(fileHash))
                {
                    filesToRemove.Add(filePath);
                }
                else
                {
                    uniqueFiles.Add(filePath, fileHash);
                }
            }

            // Удаляем дубли из ListBox
            foreach (var file in filesToRemove)
            {
                listBox1.Items.Remove(file); // Удаляем файл по пути
                File.Delete(file);
            }

            // Обновляем список файлов после удаления дублей
            MessageBox.Show($"Удалены дубли. Осталось {listBox1.Items.Count} файлов.");
        }
    }
}
