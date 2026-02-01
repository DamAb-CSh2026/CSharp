using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Prog2
    {
        public static class Config
        {
            public const string ConnectionString =
                "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
        }

        public class DatabaseService
        {
            public void Connect()
            {
                string connectionString = Config.ConnectionString;
                // Логика подключения к базе данных
            }
        }
        public class LoggingService
        {
            public void Log(string message)
            {
                string connectionString = Config.ConnectionString;
                // Логика записи лога в базу данных
            }
        }
    }
}
