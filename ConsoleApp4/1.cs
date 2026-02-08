using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Order
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
    public class Calculate
    {
        public double CalculateTotalPrice()
        {

            // Рассчет стоимости с учетом скидок

            return Order.Quantity * Order.Price * 0.9;

        }
    }
    public class Payment
    {
        public void ProcessPayment(string paymentDetails)

        {

            // Логика обработки платежа

            Console.WriteLine("Payment processed using: " + paymentDetails);

        }
    }
    public class Email
    {
        public void SendConfirmationEmail(string email)
        {

            // Логика отправки уведомления

            Console.WriteLine("Confirmation email sent to: " + email);

        }
    }
}
