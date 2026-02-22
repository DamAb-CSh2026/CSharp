using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SingletonPattern
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }

    public sealed class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> _instance =
            new Lazy<ConfigurationManager>(() => new ConfigurationManager());

        private Dictionary<string, string> _settings;
        private bool _isLoaded = false;

        private ConfigurationManager()
        {
            _settings = new Dictionary<string, string>();
            Console.WriteLine($"[Поток {Thread.CurrentThread.ManagedThreadId}] Создан экземпляр ConfigurationManager");
        }

        public static ConfigurationManager GetInstance()
        {
            return _instance.Value;
        }

        public void LoadFromFile(string filePath = "config.txt")
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllLines(filePath, new[]
                    {
                        "ServerName=localhost",
                        "Port=8080",
                        "Database=MyDB"
                    });
                }

                _settings.Clear();
                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            _settings[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }
                _isLoaded = true;
                Console.WriteLine($"Настройки загружены из {filePath}");
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Ошибка загрузки: {ex.Message}");
            }
        }

        public void SaveToFile(string filePath = "config.txt")
        {
            try
            {
                var lines = new List<string>();
                foreach (var setting in _settings)
                {
                    lines.Add($"{setting.Key}={setting.Value}");
                }
                File.WriteAllLines(filePath, lines);
                Console.WriteLine($"Настройки сохранены в {filePath}");
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Ошибка сохранения: {ex.Message}");
            }
        }

        public void LoadFromDatabase(string connectionString)
        {
            _settings = new Dictionary<string, string>
            {
                ["DbServer"] = "dbserver.com",
                ["DbName"] = "AppDB",
                ["DbUser"] = "admin"
            };
            _isLoaded = true;
            Console.WriteLine($"Настройки загружены из БД: {connectionString}");
        }

        public string GetSetting(string key)
        {
            if (!_isLoaded)
                throw new ConfigurationException("Конфигурация не загружена");

            if (_settings.ContainsKey(key))
                return _settings[key];

            throw new ConfigurationException($"Настройка '{key}' не найдена");
        }

        public string GetSetting(string key, string defaultValue)
        {
            try
            {
                return GetSetting(key);
            }
            catch
            {
                return defaultValue;
            }
        }

        public void SetSetting(string key, string value)
        {
            _settings[key] = value;
            Console.WriteLine($"Установлена настройка: {key} = {value}");
        }

        public bool HasSetting(string key)
        {
            return _isLoaded && _settings.ContainsKey(key);
        }

        public void ShowAllSettings()
        {
            if (!_isLoaded)
            {
                Console.WriteLine("Конфигурация не загружена");
                return;
            }

            Console.WriteLine("Все настройки:");
            foreach (var setting in _settings)
            {
                Console.WriteLine($"  {setting.Key} = {setting.Value}");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ПАТТЕРН SINGLETON ===\n");

            Console.WriteLine("Тест 1: Получение экземпляров");
            var config1 = ConfigurationManager.GetInstance();
            var config2 = ConfigurationManager.GetInstance();

            Console.WriteLine($"config1 hash: {config1.GetHashCode()}");
            Console.WriteLine($"config2 hash: {config2.GetHashCode()}");
            Console.WriteLine($"Это один объект? {ReferenceEquals(config1, config2)}\n");

            Console.WriteLine("Тест 2: Загрузка и изменение настроек");
            config1.LoadFromFile();
            config1.ShowAllSettings();

            Console.WriteLine("\nИзменяем порт...");
            config1.SetSetting("Port", "9090");
            Console.WriteLine($"Новый порт: {config1.GetSetting("Port")}");

            Console.WriteLine($"\nНесуществующая настройка (по умолчанию): {config1.GetSetting("Timeout", "30")}");

            config1.SaveToFile("new_config.txt");
            Console.WriteLine("\nНастройки сохранены в new_config.txt");

            Console.WriteLine("\nТест 3: Загрузка из БД");
            config1.LoadFromDatabase("Server=localhost;Database=Test");
            config1.ShowAllSettings();

            Console.WriteLine("\n=== ТЕСТ МНОГОПОТОЧНОСТИ ===");

            Thread[] threads = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                int threadNum = i;
                threads[i] = new Thread(() =>
                {
                    var config = ConfigurationManager.GetInstance();
                    Console.WriteLine($"Поток {threadNum}: получил экземпляр {config.GetHashCode()}");

                    if (threadNum == 0)
                        config.LoadFromFile();
                    else if (threadNum == 1)
                        config.SetSetting($"Thread_{threadNum}_Key", $"Value_{threadNum}");
                    else
                        Console.WriteLine($"Поток {threadNum}: Server = {config.GetSetting("ServerName", "unknown")}");
                });
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("\nВсе тесты завершены!");
            Console.ReadLine();
        }
    }
}