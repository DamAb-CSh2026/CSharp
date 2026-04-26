using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T12
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Система бронирования мероприятия ===");

            Console.Write("Введите дату мероприятия: ");
            string date = Console.ReadLine();

            Console.Write("Площадка свободна? (да/нет): ");
            string available = Console.ReadLine();

            if (available.ToLower() != "да")
            {
                Console.WriteLine("Площадка занята. Выберите другую дату или площадку.");
                return;
            }

            Console.WriteLine("Площадка доступна.");
            Console.WriteLine("Стоимость аренды: 50000 тг");

            Console.Write("Подтвердить бронирование? (да/нет): ");
            string confirm = Console.ReadLine();

            if (confirm.ToLower() != "да")
            {
                Console.WriteLine("Бронирование отменено.");
                return;
            }

            Console.Write("Оплата прошла успешно? (да/нет): ");
            string payment = Console.ReadLine();

            if (payment.ToLower() != "да")
            {
                Console.WriteLine("Оплата отклонена. Повторите попытку.");
                return;
            }

            Console.WriteLine("Бронирование подтверждено.");
            Console.WriteLine("Администратор уведомлен.");

            Console.WriteLine("Созданы задачи: декорации, еда, оборудование.");
            Console.WriteLine("Подрядчики уведомлены.");

            Console.Write("Все задачи выполнены? (да/нет): ");
            string tasks = Console.ReadLine();

            if (tasks.ToLower() == "да")
            {
                Console.WriteLine("Подготовка завершена.");
            }
            else
            {
                Console.WriteLine("Есть невыполненные задачи.");
            }

            Console.WriteLine("Мероприятие проведено.");
            Console.Write("Оцените сервис от 1 до 5: ");
            string rating = Console.ReadLine();

            Console.WriteLine("Спасибо за отзыв!");
            Console.WriteLine("Отчет отправлен менеджеру.");
        }
    }
