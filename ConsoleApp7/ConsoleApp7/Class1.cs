using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class ConfigurationManager
    {
        private static ConfigurationManager _instance;
        private static readonly object _lock = new object();
        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        private string _file = "config.txt";

        private ConfigurationManager()
        {
            if (File.Exists(_file))
            {
                foreach (string line in File.ReadAllLines(_file))
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2) _settings[parts[0]] = parts[1];
                }
            }
            else
            {
                _settings["Theme"] = "Light";
                _settings["Lang"] = "RU";
                Save();
            }
        }

        public static ConfigurationManager GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null) _instance = new ConfigurationManager();
                return _instance;
            }
        }

        public void Save()
        {
            List<string> lines = new List<string>();
            foreach (var item in _settings) lines.Add($"{item.Key}={item.Value}");
            File.WriteAllLines(_file, lines);
        }

        public string Get(string key)
        {
            return _settings.ContainsKey(key) ? _settings[key] : "Не найдено";
        }

        public void Set(string key, string value)
        {
            _settings[key] = value;
            Save();
        }

        public void Show()
        {
            Console.WriteLine("\nНастройки:");
            foreach (var item in _settings) Console.WriteLine($"{item.Key}: {item.Value}");
        }

        public void LoadDB()
        {
            _settings["DB"] = "MySQL";
            _settings["Port"] = "3306";
            Save();
            Console.WriteLine("Загружено из БД");
        }
    }
}
