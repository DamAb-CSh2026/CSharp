using System;

namespace StrategyPatternDemo
{
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }


    public class CreditCardPayment : IPaymentStrategy
    {
        private string cardNumber;

        public CreditCardPayment(string cardNumber)
        {
            this.cardNumber = cardNumber;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата {amount} тг банковской картой {cardNumber}");
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private string email;

        public PayPalPayment(string email)
        {
            this.email = email;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата {amount} тг через PayPal аккаунт {email}");
        }
    }

    public class CryptoPayment : IPaymentStrategy
    {
        private string walletAddress;

        public CryptoPayment(string walletAddress)
        {
            this.walletAddress = walletAddress;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата {amount} тг криптовалютой с кошелька {walletAddress}");
        }
    }

    public class PaymentContext
    {
        private IPaymentStrategy _paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void ExecutePayment(decimal amount)
        {
            if (_paymentStrategy == null)
            {
                Console.WriteLine("Способ оплаты не выбран!");
                return;
            }

            _paymentStrategy.Pay(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PaymentContext context = new PaymentContext();

            Console.WriteLine("Выберите способ оплаты:");
            Console.WriteLine("1 - Банковская карта");
            Console.WriteLine("2 - PayPal");
            Console.WriteLine("3 - Криптовалюта");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    context.SetPaymentStrategy(new CreditCardPayment("1234-5678-9999-0000"));
                    break;

                case "2":
                    context.SetPaymentStrategy(new PayPalPayment("user@mail.com"));
                    break;

                case "3":
                    context.SetPaymentStrategy(new CryptoPayment("0xA45F9B88C"));
                    break;

                default:
                    Console.WriteLine("Неверный выбор");
                    return;
            }

            context.ExecutePayment(5000);
        }
    }
}