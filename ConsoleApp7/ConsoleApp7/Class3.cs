using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public Product Clone()
        {
            return new Product(Name, Price, Quantity);
        }
    }
    public class Discount
    {
        public string Name { get; set; }
        public double Percent { get; set; }

        public Discount(string name, double percent)
        {
            Name = name;
            Percent = percent;
        }

        public Discount Clone()
        {
            return new Discount(Name, Percent);
        }
    }
    public class Order
    {
        public int Number { get; set; }
        public List<Product> Products { get; set; }
        public double Delivery { get; set; }
        public Discount Discount { get; set; }
        public string Payment { get; set; }

        public Order(int number)
        {
            Number = number;
            Products = new List<Product>();
        }

        public Order Clone()
        {
            Order newOrder = new Order(this.Number);

            foreach (Product p in this.Products)
            {
                newOrder.Products.Add(p.Clone());
            }

            if (this.Discount != null)
            {
                newOrder.Discount = this.Discount.Clone();
            }

            newOrder.Delivery = this.Delivery;
            newOrder.Payment = this.Payment;

            return newOrder;
        }

        public void Show()
        {
            Console.WriteLine($"\nЗаказ №{Number}:");
            double sum = 0;
            foreach (Product p in Products)
            {
                Console.WriteLine($"  {p.Name} - {p.Price} руб. x {p.Quantity}");
                sum += p.Price * p.Quantity;
            }
            Console.WriteLine($"  Доставка: {Delivery} руб.");
            if (Discount != null) Console.WriteLine($"  Скидка: {Discount.Percent}%");
            Console.WriteLine($"  Оплата: {Payment}");

            double total = sum + Delivery;
            if (Discount != null) total = total * (1 - Discount.Percent / 100);
            Console.WriteLine($"  Итого: {total} руб.");
        }
    }
}
