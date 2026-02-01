using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Prog5
    {
        public class User
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }
        public class UserRepository
        {
            public void Save(User user)
            {
                // Код для сохранения пользователя в базу данных
            }
        }
        public class EmailService
        {
            public void SendEmail(string to, string message)
            {
                // Код для отправки электронного письма
            }
        }
        public class PrinterService
        {
            public void PrintAddressLabel(string address)
            {
                // Код для печати адресного ярлыка
            }
        }
    }
}
