using System;
using System.Collections.Generic;

namespace ObserverPatternDemo
{
    public interface IObserver
    {
        void Update(string currency, decimal rate);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string currency, decimal rate);
    }

    public class CurrencyExchange : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private Dictionary<string, decimal> _rates = new Dictionary<string, decimal>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void SetRate(string currency, decimal rate)
        {
            _rates[currency] = rate;
            Console.WriteLine($"\nКурс {currency} обновлён: {rate}");
            Notify(currency, rate);
        }

        public void Notify(string currency, decimal rate)
        {
            foreach (var observer in _observers)
            {
                observer.Update(currency, rate);
            }
        }
    }


    public class BankObserver : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"[Банк] Новый курс {currency}: {rate}");
        }
    }

    public class InvestorObserver : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            if (rate > 500)
                Console.WriteLine($"[Инвестор] Курс высокий! Возможно продавать {currency}");
            else
                Console.WriteLine($"[Инвестор] Курс низкий. Можно покупать {currency}");
        }
    }

    public class MobileAppObserver : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"[Мобильное приложение] Push-уведомление: {currency} = {rate}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CurrencyExchange exchange = new CurrencyExchange();

            IObserver bank = new BankObserver();
            IObserver investor = new InvestorObserver();
            IObserver app = new MobileAppObserver();

            exchange.Attach(bank);
            exchange.Attach(investor);
            exchange.Attach(app);

            exchange.SetRate("USD", 495);
            exchange.SetRate("USD", 510);

            exchange.Detach(bank);

            exchange.SetRate("EUR", 530);

            Console.ReadLine();
        }
    }
}