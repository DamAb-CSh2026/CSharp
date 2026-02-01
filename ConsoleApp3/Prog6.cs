using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Prog6
    {
        public class FileReader
        {
            public string ReadFile(string filePath)
            {
                // Простая реализация чтения файла
                return File.ReadAllText(filePath);
            }
        }
    }
}
