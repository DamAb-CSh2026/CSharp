using System;
using System.Collections.Generic;
using System.Linq;

namespace PrototypePattern
{
    public interface IMyCloneable
    {
        object Clone();
    }

    public class Product : IMyCloneable
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Product(Product other)
        {
            Name = other.Name;
            Price = other.Price;
            Quantity = other.Quantity;
        }

        public object Clone()
        {
            return new Product(this);
        }

        public override string ToString()
        {
            return $"{Name} (${Price} x {Quantity} = ${Price * Quantity})";
        }
    }

    public class Discount : IMyCloneable
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; } 

        public Discount(string name, decimal percentage)
        {
            Name = name;
            Percentage = percentage;
        }

        public Discount(Discount other)
        {
            Name = other.Name;
            Percentage = other.Percentage;
        }

        public object Clone()
        {
            return new Discount(this);
        }

        public override string ToString()
        {
            return $"{Name} ({Percentage}%)";
        }
    }

    public class Delivery : IMyCloneable
    {
        public string Address { get; set; }
        public decimal Cost { get; set; }
        public string Method { get; set; }

        public Delivery(string address, decimal cost, string method)
        {
            Address = address;
            Cost = cost;
            Method = method;
        }

        public Delivery(Delivery other)
        {
            Address = other.Address;
            Cost = other.Cost;
            Method = other.Method;
        }

        public object Clone()
        {
            return new Delivery(this);
        }

        public override string ToString()
        {
            return $"{Method} - {Address} (${Cost})";
        }
    }

    public class PaymentMethod : IMyCloneable
    {
        public string Type { get; set; } 
        public string Details { get; set; } 

        public PaymentMethod(string type, string details)
        {
            Type = type;
            Details = details;
        }

        public PaymentMethod(PaymentMethod other)
        {
            Type = other.Type;
            Details = other.Details;
        }

        public object Clone()
        {
            return new PaymentMethod(this);
        }

        public override string ToString()
        {
            return $"{Type} ({Details})";
        }
    }

    public class Order : IMyCloneable
    {
        public int OrderNumber { get; set; }
        public List<Product> Products { get; set; }
        public Delivery Delivery { get; set; }
        public List<Discount> Discounts { get; set; }
        public PaymentMethod Payment { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }

        public Order()
        {
            Products = new List<Product>();
            Discounts = new List<Discount>();
            OrderDate = DateTime.Now;
        }

        public Order(Order other)
        {
            OrderNumber = other.OrderNumber;
            CustomerName = other.CustomerName;
            OrderDate = other.OrderDate;

            Products = new List<Product>();
            foreach (var product in other.Products)
            {
                Products.Add((Product)product.Clone());
            }

            Delivery = other.Delivery != null ? (Delivery)other.Delivery.Clone() : null;

            Discounts = new List<Discount>();
            foreach (var discount in other.Discounts)
            {
                Discounts.Add((Discount)discount.Clone());
            }

            Payment = other.Payment != null ? (PaymentMethod)other.Payment.Clone() : null;
        }

        public object Clone()
        {
            return new Order(this);
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void AddDiscount(Discount discount)
        {
            Discounts.Add(discount);
        }

        public decimal CalculateTotal()
        {
            decimal subtotal = Products.Sum(p => p.Price * p.Quantity);

            decimal discountAmount = 0;
            foreach (var discount in Discounts)
            {
                discountAmount += subtotal * (discount.Percentage / 100);
            }

            decimal deliveryCost = Delivery?.Cost ?? 0;

            return subtotal - discountAmount + deliveryCost;
        }

        public void DisplayOrderInfo(string title = "ЗАКАЗ")
        {
            Console.WriteLine($"\n=== {title} #{OrderNumber} ===");
            Console.WriteLine($"Клиент: {CustomerName}");
            Console.WriteLine($"Дата: {OrderDate}");
            Console.WriteLine("\nТовары:");

            foreach (var product in Products)
            {
                Console.WriteLine($"  {product}");
            }

            if (Delivery != null)
            {
                Console.WriteLine($"\nДоставка: {Delivery}");
            }

            if (Discounts.Any())
            {
                Console.WriteLine("\nСкидки:");
                foreach (var discount in Discounts)
                {
                    Console.WriteLine($"  {discount}");
                }
            }

            if (Payment != null)
            {
                Console.WriteLine($"\nОплата: {Payment}");
            }

            Console.WriteLine($"\nИТОГО: ${CalculateTotal():F2}");
            Console.WriteLine(new string('-', 40));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ПАТТЕРН PROTOTYPE (Прототип) ===\n");

            Console.WriteLine("ТЕСТ 1: Создание базового заказа");

            var baseOrder = new Order
            {
                OrderNumber = 1001,
                CustomerName = "Иван Петров"
            };

            baseOrder.AddProduct(new Product("Ноутбук", 1200, 1));
            baseOrder.AddProduct(new Product("Мышь", 25, 2));
            baseOrder.AddProduct(new Product("Коврик", 15, 1));

            baseOrder.Delivery = new Delivery("ул. Ленина, 10", 10, "Курьер");

            baseOrder.AddDiscount(new Discount("Сезонная", 10));
            baseOrder.AddDiscount(new Discount("Постоянный клиент", 5));

            baseOrder.Payment = new PaymentMethod("Карта", "****1234");

            baseOrder.DisplayOrderInfo("БАЗОВЫЙ ЗАКАЗ");

            Console.WriteLine("\nТЕСТ 2: Клонирование заказа (глубокое клонирование)");

            var clonedOrder = (Order)baseOrder.Clone();
            clonedOrder.OrderNumber = 1002;
            clonedOrder.CustomerName = "Анна Сидорова";

            clonedOrder.DisplayOrderInfo("КЛОН ЗАКАЗА");

            Console.WriteLine("\nТЕСТ 3: Изменение клона не влияет на оригинал");

            clonedOrder.Products[0].Price = 1500; 
            clonedOrder.Delivery.Address = "ул. Пушкина, 5";
            clonedOrder.Discounts[0].Percentage = 20; 
            clonedOrder.Payment.Details = "****5678";

            Console.WriteLine("ПОСЛЕ ИЗМЕНЕНИЯ КЛОНА:");
            baseOrder.DisplayOrderInfo("ОРИГИНАЛ (НЕ ИЗМЕНИЛСЯ)");
            clonedOrder.DisplayOrderInfo("КЛОН (ИЗМЕНЕН)");

            Console.WriteLine("\nТЕСТ 4: Создание заказа на основе прототипа с изменениями");

            var anotherOrder = (Order)baseOrder.Clone();
            anotherOrder.OrderNumber = 1003;
            anotherOrder.CustomerName = "Петр Иванов";

            anotherOrder.AddProduct(new Product("Клавиатура", 80, 1));

            anotherOrder.Discounts.RemoveAt(1);

            anotherOrder.DisplayOrderInfo("ЗАКАЗ НА ОСНОВЕ ПРОТОТИПА");

            Console.WriteLine("\nТЕСТ 5: Проверка глубокого клонирования");

            var testOrder = new Order
            {
                OrderNumber = 1004,
                CustomerName = "Тестовый клиент"
            };

            var phone = new Product("Телефон", 500, 1);
            testOrder.AddProduct(phone);
            testOrder.Delivery = new Delivery("Тестовый адрес", 5, "Почта");
            testOrder.Payment = new PaymentMethod("Наличные", "При получении");

            var testClone = (Order)testOrder.Clone();

            phone.Price = 1000;
            testOrder.Delivery.Cost = 20;
            testOrder.Payment.Type = "Карта";

            Console.WriteLine("После изменения оригинала:");
            testOrder.DisplayOrderInfo("ОРИГИНАЛ (ИЗМЕНЕН)");
            testClone.DisplayOrderInfo("КЛОН (НЕ ИЗМЕНИЛСЯ)");

            Console.WriteLine("\nВсе тесты завершены!");
            Console.ReadLine();
        }
    }
}