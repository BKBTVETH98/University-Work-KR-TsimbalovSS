using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRTsimbalov.Class
{
    static internal class Algoritm
    {
        static public List<string> filePaths = new List<string>();
        static public Dictionary<string, string> uniqueFiles = new Dictionary<string, string>();
        static public Dictionary<string, List<string>> fileComparisons = new Dictionary<string, List<string>>();
        static public string GetFileHash(string filePath)
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
        static public bool AreFilesIdentical(string filePath1, string filePath2)
        {
            byte[] file1Bytes = File.ReadAllBytes(filePath1);
            byte[] file2Bytes = File.ReadAllBytes(filePath2);

            if (file1Bytes.Length != file2Bytes.Length)
                return false; // Файлы не идентичны по длине

            for (int i = 0; i < file1Bytes.Length; i++)
            {
                if (file1Bytes[i] != file2Bytes[i])
                    return false; // Если есть отличие в содержимом
            }

            return true; // Файлы идентичны
        }
        public static void CleanData(ListBox list1, ListBox list2, List<string> FilePath, Dictionary<string, List<string>> fileComparisons)
        {
            MessageBox.Show("Список файлов изменен после удаления");
            list1.Items.Clear();
            list2.Items.Clear();
            fileComparisons.Clear();
            FilePath.Clear();
        }
    }
}
