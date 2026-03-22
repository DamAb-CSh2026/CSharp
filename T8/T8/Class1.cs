using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T8
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Оплата {amount} через PayPal выполнена");
        }
    }

    public class StripePaymentService
    {
        public void MakeTransaction(double totalAmount)
        {
            Console.WriteLine($"Stripe провёл транзакцию на {totalAmount}");
        }
    }

    public class StripePaymentAdapter : IPaymentProcessor
    {
        private StripePaymentService stripe = new StripePaymentService();

        public void ProcessPayment(double amount)
        {
            stripe.MakeTransaction(amount);
        }
    }

    public class CryptoPaymentService
    {
        public void SendCrypto(double sum)
        {
            Console.WriteLine($"Крипто-платёж на {sum} отправлен");
        }
    }

    public class CryptoPaymentAdapter : IPaymentProcessor
    {
        private CryptoPaymentService crypto = new CryptoPaymentService();

        public void ProcessPayment(double amount)
        {
            crypto.SendCrypto(amount);
        }
    }

    public class Program
    {
        public static void Main()
        {
            IPaymentProcessor paypal = new PayPalPaymentProcessor();
            IPaymentProcessor stripe = new StripePaymentAdapter();
            IPaymentProcessor crypto = new CryptoPaymentAdapter();

            paypal.ProcessPayment(1000);
            stripe.ProcessPayment(2000);
            crypto.ProcessPayment(3000);
        }
    }
}
