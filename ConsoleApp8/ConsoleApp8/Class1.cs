using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public interface IObserver
    {
        void Update(double rate);
    }

    public interface ISubject
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify();
    }

    public class CurrencyExchange : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private double rate;

        public void SetRate(double newRate)
        {
            rate = newRate;
            Notify();
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
                observer.Update(rate);
        }
    }
    public class Bank : IObserver
    {
        public void Update(double rate)
        {
            Console.WriteLine($"Банк получил новый курс: {rate}");
        }
    }
    public class Investor : IObserver
    {
        public void Update(double rate)
        {
            Console.WriteLine($"Инвестор анализирует курс: {rate}");
        }
    }
    public class MobileApp : IObserver
    {
        public void Update(double rate)
        {
            Console.WriteLine($"Мобильное приложение уведомляет пользователя: {rate}");
        }
    }
    class Program
    {
        static void Main()
        {
            CurrencyExchange exchange = new CurrencyExchange();

            IObserver bank = new Bank();
            IObserver investor = new Investor();
            IObserver app = new MobileApp();

            exchange.AddObserver(bank);
            exchange.AddObserver(investor);
            exchange.AddObserver(app);

            Console.WriteLine("Изменение курса...");
            exchange.SetRate(470.5);

            Console.WriteLine("\nУдаляем инвестора...\n");
            exchange.RemoveObserver(investor);

            exchange.SetRate(480.0);
        }
    }
}
