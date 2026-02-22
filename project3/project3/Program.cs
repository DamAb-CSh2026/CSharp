using System;

namespace SRP_Example
{
    public class Order
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class OrderCalculator
    {
        public double CalculateTotalPrice(Order order)
        {
            return order.Quantity * order.Price * 0.9;
        }
    }

    public class PaymentProcessor
    {
        public void ProcessPayment(string paymentDetails)
        {
            Console.WriteLine("Платеж обработан через: " + paymentDetails);
        }
    }

    public class EmailService
    {
        public void SendConfirmationEmail(string email)
        {
            Console.WriteLine("Подтверждение отправлено на email: " + email);
        }
    }

    public class OrderService
    {
        private OrderCalculator _calculator = new OrderCalculator();
        private PaymentProcessor _paymentProcessor = new PaymentProcessor();
        private EmailService _emailService = new EmailService();

        public void ProcessOrder(Order order, string paymentDetails, string email)
        {
            double total = _calculator.CalculateTotalPrice(order);
            Console.WriteLine($"Общая стоимость заказа: {total}");

            _paymentProcessor.ProcessPayment(paymentDetails);

            _emailService.SendConfirmationEmail(email);

            Console.WriteLine("Заказ успешно обработан!");
        }
    }

    class Program
    {
        static void Main()
        {
            var order = new Order
            {
                ProductName = "Ноутбук",
                Quantity = 2,
                Price = 50000
            };

            var orderService = new OrderService();
            orderService.ProcessOrder(order, "Карта 1234", "customer@mail.com");

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}