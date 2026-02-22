using System;
using System.Collections.Generic;
using System.Linq;

namespace Principles_DRY_KISS_YAGNI
{

    public class Logger
    {
        public void Log(string level, string message)
        {
            Console.WriteLine($"{level}: {message}");
        }

        public void LogError(string message) => Log("ERROR", message);
        public void LogWarning(string message) => Log("WARNING", message);
        public void LogInfo(string message) => Log("INFO", message);
    }

    public static class AppConfig
    {
        public static string ConnectionString { get; set; } =
            "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
    }

    public class DatabaseService
    {
        public void Connect()
        {
            string connStr = AppConfig.ConnectionString;
            Console.WriteLine($"Подключение к БД: {connStr}");
        }
    }

    public class LoggingService
    {
        public void Log(string message)
        {
            string connStr = AppConfig.ConnectionString;
            Console.WriteLine($"Запись лога в БД: {connStr}");
            Console.WriteLine($"Сообщение: {message}");
        }
    }


    public class NumberProcessor
    {
        public void ProcessNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                Console.WriteLine("Нет чисел для обработки");
                return;
            }

            foreach (var number in numbers)
            {
                if (number > 0)
                {
                    Console.WriteLine(number);
                }
            }
        }
    }

    public class PositiveNumberPrinter
    {
        public void PrintPositiveNumbers(int[] numbers)
        {
            if (numbers == null) return;

            foreach (var n in numbers)
            {
                if (n > 0) Console.WriteLine(n);
            }
        }
    }

    public class SimpleCalculator
    {
        public int Divide(int a, int b)
        {
            if (b == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль");
                return 0;
            }

            return a / b;
        }
    }


    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public void SaveToDatabase()
        {
            Console.WriteLine($"Пользователь {Name} сохранен в БД");
        }

    }

    public class FileReader
    {
        public string ReadFile(string filePath)
        {
            Console.WriteLine($"Чтение файла: {filePath}");
            return "содержимое файла";
        }
    }

    public class ReportGenerator
    {
        public void GenerateReport(string data)
        {
            Console.WriteLine($"Генерация отчета: {data}");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== ПРИНЦИП DRY ===\n");

            var logger = new Logger();
            logger.LogError("Ошибка подключения");
            logger.LogWarning("Низкий заряд батареи");
            logger.LogInfo("Программа запущена");

            Console.WriteLine();

            var dbService = new DatabaseService();
            dbService.Connect();

            var logService = new LoggingService();
            logService.Log("Тестовое сообщение");

            Console.WriteLine("\n=== ПРИНЦИП KISS ===\n");

            var numbers = new int[] { 1, -2, 3, 0, 5, -1 };

            var processor = new NumberProcessor();
            Console.WriteLine("Обработка чисел:");
            processor.ProcessNumbers(numbers);

            Console.WriteLine();

            var printer = new PositiveNumberPrinter();
            Console.WriteLine("Положительные числа:");
            printer.PrintPositiveNumbers(numbers);

            Console.WriteLine();

            var calc = new SimpleCalculator();
            Console.WriteLine($"10 / 2 = {calc.Divide(10, 2)}");
            Console.WriteLine($"10 / 0 = {calc.Divide(10, 0)}");

            Console.WriteLine("\n=== ПРИНЦИП YAGNI ===\n");

            var user = new User { Name = "Иван", Email = "ivan@mail.com" };
            user.SaveToDatabase();

            Console.WriteLine();

            var reader = new FileReader();
            reader.ReadFile("test.txt");

            Console.WriteLine();

            var reportGen = new ReportGenerator();
            reportGen.GenerateReport("Ежемесячный отчет");

            Console.WriteLine("\n=== ОБЪЯСНЕНИЕ ===\n");

            Console.WriteLine("DRY (Don't Repeat Yourself):");
            Console.WriteLine("- Один метод Log() вместо трех похожих");
            Console.WriteLine("- Одна строка подключения в AppConfig");

            Console.WriteLine("\nKISS (Keep It Simple, Stupid):");
            Console.WriteLine("- Убраны лишние вложенные if");
            Console.WriteLine("- Убраны избыточные LINQ операции");
            Console.WriteLine("- Простая проверка вместо try-catch");

            Console.WriteLine("\nYAGNI (You Ain't Gonna Need It):");
            Console.WriteLine("- User только с нужными полями");
            Console.WriteLine("- FileReader без лишних параметров");
            Console.WriteLine("- ReportGenerator только с одним форматом");

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}