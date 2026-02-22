using System;
using System.Collections.Generic;

namespace ISP_Example
{

    public interface IPrinter
    {
        void Print(string content);
    }

    public interface IScanner
    {
        void Scan(string content);
    }

    public interface IFax
    {
        void Fax(string content);
    }

    public interface ICopier
    {
        void Copy(string content);
    }

    public interface IEmailSender
    {
        void SendEmail(string content, string email);
    }


    public class BasicPrinter : IPrinter
    {
        public void Print(string content)
        {
            Console.WriteLine($"[Простой принтер] Печать: {content}");
        }
    }

    public class MultiFunctionPrinter : IPrinter, IScanner
    {
        public void Print(string content)
        {
            Console.WriteLine($"[МФУ] Печать: {content}");
        }

        public void Scan(string content)
        {
            Console.WriteLine($"[МФУ] Сканирование: {content}");
        }
    }

    public class OfficePrinter : IPrinter, IScanner, IFax, ICopier
    {
        public void Print(string content)
        {
            Console.WriteLine($"[Офисный комбайн] Печать: {content}");
        }

        public void Scan(string content)
        {
            Console.WriteLine($"[Офисный комбайн] Сканирование: {content}");
        }

        public void Fax(string content)
        {
            Console.WriteLine($"[Офисный комбайн] Отправка факса: {content}");
        }

        public void Copy(string content)
        {
            Console.WriteLine($"[Офисный комбайн] Копирование: {content}");
        }
    }

    public class NetworkPrinter : IPrinter, IEmailSender
    {
        public void Print(string content)
        {
            Console.WriteLine($"[Сетевой принтер] Печать: {content}");
        }

        public void SendEmail(string content, string email)
        {
            Console.WriteLine($"[Сетевой принтер] Отправка на email {email}: {content}");
        }
    }

    public class SimpleScanner : IScanner
    {
        public void Scan(string content)
        {
            Console.WriteLine($"[Сканер] Сканирование: {content}");
        }
    }

    public class PrinterManager
    {
        private List<IPrinter> _printers = new List<IPrinter>();
        private List<IScanner> _scanners = new List<IScanner>();

        public void AddPrinter(IPrinter printer)
        {
            _printers.Add(printer);
            Console.WriteLine($"Принтер добавлен: {printer.GetType().Name}");
        }

        public void AddScanner(IScanner scanner)
        {
            _scanners.Add(scanner);
            Console.WriteLine($"Сканер добавлен: {scanner.GetType().Name}");
        }

        public void PrintAll(string content)
        {
            Console.WriteLine("\n=== ПЕЧАТЬ НА ВСЕХ ПРИНТЕРАХ ===");
            foreach (var printer in _printers)
            {
                printer.Print(content);
            }
        }

        public void ScanAll(string content)
        {
            Console.WriteLine("\n=== СКАНИРОВАНИЕ НА ВСЕХ СКАНЕРАХ ===");
            foreach (var scanner in _scanners)
            {
                scanner.Scan(content);
            }
        }
    }

    public class PrintService
    {
        public void ExecutePrint(IPrinter printer, string content)
        {
            Console.WriteLine("Выполняется печать...");
            printer.Print(content);
        }

        public void ExecuteScan(IScanner scanner, string content)
        {
            Console.WriteLine("Выполняется сканирование...");
            scanner.Scan(content);
        }

        public void ExecuteFax(IFax fax, string content)
        {
            Console.WriteLine("Отправка факса...");
            fax.Fax(content);
        }
    }

    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== ПРИНЦИП РАЗДЕЛЕНИЯ ИНТЕРФЕЙСОВ (ISP) ===\n");

            var basicPrinter = new BasicPrinter();
            var mfuPrinter = new MultiFunctionPrinter();
            var officePrinter = new OfficePrinter();
            var networkPrinter = new NetworkPrinter();
            var simpleScanner = new SimpleScanner();

            Console.WriteLine("--- РАБОТА УСТРОЙСТВ ---");

            basicPrinter.Print("Отчет.pdf");
            Console.WriteLine();

            mfuPrinter.Print("Документ.docx");
            mfuPrinter.Scan("Фото.jpg");
            Console.WriteLine();

            officePrinter.Print("Контракт.pdf");
            officePrinter.Scan("Скан.png");
            officePrinter.Fax("Счет.pdf");
            officePrinter.Copy("Паспорт.jpg");
            Console.WriteLine();

            networkPrinter.Print("Инструкция.pdf");
            networkPrinter.SendEmail("Файл.pdf", "user@mail.com");
            Console.WriteLine();

            simpleScanner.Scan("Документ.png");

            Console.WriteLine("\n--- МЕНЕДЖЕР УСТРОЙСТВ ---");
            var manager = new PrinterManager();

            manager.AddPrinter(basicPrinter);
            manager.AddPrinter(mfuPrinter);
            manager.AddPrinter(officePrinter);
            manager.AddPrinter(networkPrinter);

            manager.AddScanner(mfuPrinter);
            manager.AddScanner(officePrinter);
            manager.AddScanner(simpleScanner);

            manager.PrintAll("Важный документ");
            manager.ScanAll("Важное фото");

            Console.WriteLine("\n--- СЕРВИС ПЕЧАТИ ---");
            var printService = new PrintService();

            printService.ExecutePrint(basicPrinter, "Текст");
            printService.ExecutePrint(officePrinter, "Текст");

 
            printService.ExecuteScan(simpleScanner, "Фото");

            Console.WriteLine("\n--- НОВАЯ ФУНКЦИОНАЛЬНОСТЬ ---");

            interface I3DPrinter
        {
            void Print3D(string model);
        }

        class ThreeDPrinter : IPrinter, I3DPrinter
        {
            public void Print(string content)
                => Console.WriteLine($"[3D принтер] Обычная печать: {content}");

            public void Print3D(string model)
                => Console.WriteLine($"[3D принтер] 3D печать модели: {model}");
        }

        var threeDPrinter = new ThreeDPrinter();
        threeDPrinter.Print("Документ");
            threeDPrinter.Print3D("Ваза");

            Console.WriteLine("\n=== ПРЕИМУЩЕСТВА ISP ===");
            Console.WriteLine("✓ Каждый класс реализует только нужные методы");
            Console.WriteLine("✓ Нет пустых заглушек (throw new NotImplementedException)");
            Console.WriteLine("✓ Интерфейсы маленькие и сфокусированные");
            Console.WriteLine("✓ Легко добавлять новую функциональность");
            Console.WriteLine("✓ Клиенты не зависят от методов, которые не используют");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
}
}