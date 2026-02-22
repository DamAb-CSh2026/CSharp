using System;
using System.Collections.Generic;

namespace DIP_Example
{
    public interface IMessageSender
    {
        void Send(string message);
        string SenderType { get; }
    }


    public class EmailSender : IMessageSender
    {
        public string SenderType => "Email";

        public void Send(string message)
        {
            Console.WriteLine($"[EMAIL] Отправлено: {message}");
            LogEmail(message);
        }

        private void LogEmail(string message)
        {
            Console.WriteLine($"[LOG] Email сохранен в базе данных");
        }
    }

    public class SmsSender : IMessageSender
    {
        public string SenderType => "SMS";

        public void Send(string message)
        {
            Console.WriteLine($"[SMS] Отправлено: {message}");
            CheckPhoneNumber();
        }

        private void CheckPhoneNumber()
        {
            Console.WriteLine($"[SMS] Номер телефона проверен");
        }
    }

    public class TelegramSender : IMessageSender
    {
        public string SenderType => "Telegram";

        public void Send(string message)
        {
            Console.WriteLine($"[TELEGRAM] Отправлено: {message}");
            EncryptMessage(message);
        }

        private void EncryptMessage(string message)
        {
            Console.WriteLine($"[TELEGRAM] Сообщение зашифровано");
        }
    }

    public class PushSender : IMessageSender
    {
        public string SenderType => "Push";

        public void Send(string message)
        {
            Console.WriteLine($"[PUSH] Отправлено: {message}");
            CheckDeviceStatus();
        }

        private void CheckDeviceStatus()
        {
            Console.WriteLine($"[PUSH] Статус устройства проверен");
        }
    }

    public class NotificationService
    {
        private readonly List<IMessageSender> _senders;

        public NotificationService(List<IMessageSender> senders)
        {
            _senders = senders ?? new List<IMessageSender>();
        }

        public void AddSender(IMessageSender sender)
        {
            _senders.Add(sender);
            Console.WriteLine($"Добавлен отправитель: {sender.SenderType}");
        }

        public void SendNotification(string message)
        {
            Console.WriteLine($"\n=== ОТПРАВКА УВЕДОМЛЕНИЯ: {message} ===\n");

            if (_senders.Count == 0)
            {
                Console.WriteLine("Нет доступных отправителей!");
                return;
            }

            foreach (var sender in _senders)
            {
                sender.Send(message);
            }
        }

        public void SendNotificationByType(string message, string senderType)
        {
            var sender = _senders.Find(s => s.SenderType.Equals(senderType, StringComparison.OrdinalIgnoreCase));

            if (sender != null)
            {
                Console.WriteLine($"\n--- Отправка через {senderType} ---");
                sender.Send(message);
            }
            else
            {
                Console.WriteLine($"Отправитель типа {senderType} не найден!");
            }
        }
    }

    public static class SenderFactory
    {
        public static IMessageSender CreateSender(string type)
        {
            return type.ToLower() switch
            {
                "email" => new EmailSender(),
                "sms" => new SmsSender(),
                "telegram" => new TelegramSender(),
                "push" => new PushSender(),
                _ => throw new ArgumentException($"Неизвестный тип отправителя: {type}")
            };
        }
    }

    public class NotificationConfig
    {
        public List<string> EnabledSenders { get; set; } = new List<string>();
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== ПРИНЦИП ИНВЕРСИИ ЗАВИСИМОСТЕЙ (DIP) ===\n");

            Console.WriteLine("--- СПОСОБ 1: Ручное создание ---\n");

            var senders = new List<IMessageSender>
            {
                new EmailSender(),
                new SmsSender()
            };

            var notificationService = new NotificationService(senders);
            notificationService.SendNotification("Привет, мир!");

            notificationService.AddSender(new TelegramSender());
            notificationService.SendNotification("Новое сообщение!");

            Console.WriteLine("\n--- СПОСОБ 2: Использование фабрики ---\n");

            var factorySenders = new List<IMessageSender>
            {
                SenderFactory.CreateSender("email"),
                SenderFactory.CreateSender("sms"),
                SenderFactory.CreateSender("telegram"),
                SenderFactory.CreateSender("push")
            };

            var factoryService = new NotificationService(factorySenders);
            factoryService.SendNotification("Сообщение из фабрики!");

            Console.WriteLine("\n--- СПОСОБ 3: Выборочная отправка ---\n");

            var selectiveService = new NotificationService(new List<IMessageSender>
            {
                new EmailSender(),
                new SmsSender(),
                new TelegramSender()
            });

            selectiveService.SendNotificationByType("Важное сообщение!", "Email");
            selectiveService.SendNotificationByType("Срочное уведомление!", "Telegram");

            Console.WriteLine("\n--- СПОСОБ 4: На основе конфигурации ---\n");

            var config = new NotificationConfig
            {
                EnabledSenders = new List<string> { "email", "push" }
            };

            var configSenders = new List<IMessageSender>();
            foreach (var senderType in config.EnabledSenders)
            {
                try
                {
                    configSenders.Add(SenderFactory.CreateSender(senderType));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            var configService = new NotificationService(configSenders);
            configService.SendNotification("Уведомление из конфига!");

            Console.WriteLine("\n=== ПРЕИМУЩЕСТВА DIP ===\n");
            Console.WriteLine("✓ Легко добавлять новые типы отправителей:");
            Console.WriteLine("  public class ViberSender : IMessageSender");
            Console.WriteLine("  {");
            Console.WriteLine("      public void Send(string message) => ...");
            Console.WriteLine("  }");
            Console.WriteLine();
            Console.WriteLine("✓ Класс NotificationService не изменяется при добавлении новых типов");
            Console.WriteLine("✓ Зависимости можно подменять для тестирования:");
            Console.WriteLine("  var testService = new NotificationService(new List<IMessageSender> { new TestSender() });");
            Console.WriteLine("✓ Гибкая конфигурация системы");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}