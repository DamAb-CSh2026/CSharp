using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp3
{
    internal class Prog7
    {
        public class PdfReportGenerator
        {
            public void Generate(ReportData data)
            {
                // Генерация PDF отчета
            }
        }

        public static void Main(string[] args)
        {
            var generator = new PdfReportGenerator();
            generator.Generate(data);
        }
    }
}
