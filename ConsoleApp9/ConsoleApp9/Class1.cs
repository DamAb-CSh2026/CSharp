using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    abstract class Beverage
    {
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();

            if (CustomerWantsCondiments())
            {
                AddCondiments();
            }
        }

        void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        void PourInCup()
        {
            Console.WriteLine("Pouring into cup");
        }

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
            Console.WriteLine("Steeping the tea");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Adding lemon");
        }
    }

    class Coffee : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Brewing coffee");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Adding sugar and milk");
        }

        protected override bool CustomerWantsCondiments()
        {
            Console.Write("Add sugar and milk? (y/n): ");
            string answer = Console.ReadLine().ToLower();

            if (answer == "y") return true;
            if (answer == "n") return false;

            Console.WriteLine("Invalid input. Condiments will not be added.");
            return false;
        }
    }

    class HotChocolate : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Mixing chocolate powder with hot water");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Adding marshmallows");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Making Tea:");
            Beverage tea = new Tea();
            tea.PrepareRecipe();

            Console.WriteLine("\nMaking Coffee:");
            Beverage coffee = new Coffee();
            coffee.PrepareRecipe();

            Console.WriteLine("\nMaking Hot Chocolate:");
            Beverage chocolate = new HotChocolate();
            chocolate.PrepareRecipe();
        }
    }
}
