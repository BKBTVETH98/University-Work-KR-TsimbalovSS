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
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true; // Разрешаем выбор нескольких файлов

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Algoritm.filePaths = new List<string>(openFileDialog.FileNames);

                    // Добавляем имена файлов в ListBox1
                    listBox1.Items.Clear();
                    foreach (var filePath in Algoritm.filePaths)
                    {
                        listBox1.Items.Add(Path.GetFileName(filePath));
                    }
                }
            }
        }

        // Сравнение файлов по хешу (MD5)
        private void btnCompareByHash_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count == 0)
                MessageBox.Show("Необходимо выбрать Файлы", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Dictionary<string, string> fileHashes = new Dictionary<string, string>();
            listBox2.Items.Clear();
            try
            {


                // Вычисляем хеши файлов
                foreach (var filePath in Algoritm.filePaths)
                {

                    string hash = Algoritm.GetFileHash(filePath);
                    fileHashes[filePath] = hash;
                    continue;
                }

                // Сравниваем файлы по хешам и выводим результат в ListBox2
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
                        listBox2.Items.Add($"{Path.GetFileName(file1)}: Отличается от всех выбранных файлов.");
                    }
                    else
                    {
                        listBox2.Items.Add($"{Path.GetFileName(file1)}: Идентичен файлам: {identicalFiles}");
                    }
                }
            }
            catch (Exception ex)
            {
                Algoritm.CleanData(listBox1,listBox2, Algoritm.filePaths, fileComparisons);
            }
        }

        // Сравнение файлов побайтово
        private void btnCompareByByte_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count == 0)
                MessageBox.Show("Необходимо выбрать Файлы", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            listBox2.Items.Clear();
            try
            {
                for (int i = 0; i < Algoritm.filePaths.Count; i++)
                {
                    string file1 = Algoritm.filePaths[i];
                    string fileName1 = Path.GetFileName(file1); // Получаем имя первого файла

                    // Инициализируем список для хранения идентичных файлов
                    if (!fileComparisons.ContainsKey(file1))
                    {
                        fileComparisons[file1] = new List<string>();
                    }

                    // Сравниваем файл1 со всеми остальными файлами
                    for (int j = 0; j < Algoritm.filePaths.Count; j++)
                    {
                        // Пропускаем сравнение файла с самим собой
                        if (i == j)
                            continue;

                        string file2 = Algoritm.filePaths[j];
                        string fileName2 = Path.GetFileName(file2); // Получаем имя второго файла

                        // Если файлы идентичны побайтово
                        if (Algoritm.AreFilesIdentical(file1, file2))
                        {
                            fileComparisons[file1].Add(fileName2); // Добавляем имя второго файла в список идентичных
                        }
                    }
                }
                // Сравниваем все файлы побайтово

                // Выводим результаты в ListBox2
                foreach (var fileComparison in fileComparisons)
                {
                    string fileName1 = Path.GetFileName(fileComparison.Key);
                    string identicalFiles = string.Join(", ", fileComparison.Value); // Преобразуем список в строку через запятую

                    if (string.IsNullOrEmpty(identicalFiles))
                    {
                        // Если не найдено идентичных файлов
                        listBox2.Items.Add($"{fileName1}: Отличается от всех выбранных файлов.");
                    }
                    else
                    {
                        // Если файлы идентичны хотя бы с одним
                        listBox2.Items.Add($"{fileName1}: Идентичен файлам: {identicalFiles}");
                    }
                    // Пример для побайтового сравнения
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
            // Запоминаем, что мышь зажата, и точку относительно левого верхнего угла формы
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
                // Расчет нового положения формы
                Point newLocation = this.Location;
                newLocation.X += e.X - mouseOffset.X;
                newLocation.Y += e.Y - mouseOffset.Y;
                this.Location = newLocation;
            }
        }

        // Событие для завершения перетаскивания
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false; // Останавливаем перетаскивание
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
                // Возвращаем сохранённые контролы
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