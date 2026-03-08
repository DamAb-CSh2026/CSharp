using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr7
{
    internal class Class1
    {

    abstract class Beverage
    {
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();

            if (CustomerWantsCondiments())
                AddCondiments();
        }

        void BoilWater() => Console.WriteLine("Кипятим воду");
        void PourInCup() => Console.WriteLine("Наливаем в чашку");

        protected abstract void Brew();
        protected abstract void AddCondiments();

        protected virtual bool CustomerWantsCondiments()
        {
            return true;
        }
    }

    class Tea : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Завариваем чай");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Добавляем лимон");
        }
    }

    class Coffee : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Завариваем кофе");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Добавляем сахар и молоко");
        }

        protected override bool CustomerWantsCondiments()
        {
            Console.Write("Добавить сахар и молоко? (y/n): ");
            string answer = Console.ReadLine();
            return answer.ToLower() == "y";
        }
    }

    class HotChocolate : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Готовим горячий шоколад");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Добавляем маршмеллоу");
        }
    }

    class Program
    {
        static void Main()
        {
            Beverage tea = new Tea();
            Beverage coffee = new Coffee();
            Beverage chocolate = new HotChocolate();

            Console.WriteLine("Чай:");
            tea.PrepareRecipe();

            Console.WriteLine("\nКофе:");
            coffee.PrepareRecipe();

            Console.WriteLine("\nГорячий шоколад:");
            chocolate.PrepareRecipe();
        }
    }
}
}
