using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public interface NotificationType
    {
        void SendMessage(string message)
    }
    public class EmailSender:NotificationType

    {

        public void SendMessage(string message)

        {

            Console.WriteLine("Email sent: " + message);

        }

    }


    public class SmsSender:NotificationType

    {

        public void SendMessage(string message)

        {

            Console.WriteLine("SMS sent: " + message);

        }

    }


    public class NotificationService

    {

        NotificationType notification;

        public NotificationService(NotificationType notification)
        public void SendNotification(string message)

        {
            notification.SendMessage(message)
        }

    }
}
