using System;

public interface IOrder
{
    void Process();
    void ShowDetails();
    double CalculateTotal();
    void GenerateInvoice();
}

public class PhysicalProductOrder : IOrder
{
    private string _deliveryAddress;
    private string _status;
    private double _price;
    private double _deliveryCost;

    public PhysicalProductOrder(string deliveryAddress, double price, double deliveryCost)
    {
        _deliveryAddress = deliveryAddress;
        _status = "На складе";
        _price = price;
        _deliveryCost = deliveryCost;
    }

    public void Process() => Console.WriteLine("Оформляем доставку физического товара...");

    public void ShowDetails() => Console.WriteLine($"Физический товар | Адрес: {_deliveryAddress} | Статус: {_status}");

    public double CalculateTotal() => _price + _deliveryCost;

    public void GenerateInvoice()
    {
        Console.WriteLine("=== ИНВОЙС (Физический товар) ===");
        Console.WriteLine($"Товар: {_price} тг");
        Console.WriteLine($"Доставка: {_deliveryCost} тг");
        Console.WriteLine($"ИТОГО: {CalculateTotal()} тг");
        Console.WriteLine($"Адрес доставки: {_deliveryAddress}");
    }
}

public class DigitalProductOrder : IOrder
{
    private string _licenseKey;
    private string _format;
    private double _price;
    private double _tax;

    public DigitalProductOrder(string licenseKey, string format, double price, double tax)
    {
        _licenseKey = licenseKey;
        _format = format;
        _price = price;
        _tax = tax;
    }

    public void Process() => Console.WriteLine("Активируем лицензию на цифровой товар...");

    public void ShowDetails() => Console.WriteLine($"Цифровой товар | Лицензия: {_licenseKey} | Формат: {_format}");

    public double CalculateTotal() => _price + _tax;

    public void GenerateInvoice()
    {
        Console.WriteLine("=== ИНВОЙС (Цифровой товар) ===");
        Console.WriteLine($"Товар: {_price} тг");
        Console.WriteLine($"Налог: {_tax} тг");
        Console.WriteLine($"ИТОГО: {CalculateTotal()} тг");
        Console.WriteLine($"Лицензия: {_licenseKey}");
    }
}

public class SubscriptionOrder : IOrder
{
    private int _durationMonths;
    private string _type;
    private double _monthlyPrice;

    public SubscriptionOrder(int durationMonths, string type, double monthlyPrice)
    {
        _durationMonths = durationMonths;
        _type = type;
        _monthlyPrice = monthlyPrice;
    }

    public void Process() => Console.WriteLine("Оформляем подписку...");

    public void ShowDetails() => Console.WriteLine($"Подписка | Тип: {_type} | Срок: {_durationMonths} мес.");

    public double CalculateTotal() => _durationMonths * _monthlyPrice;

    public void GenerateInvoice()
    {
        Console.WriteLine("=== ИНВОЙС (Подписка) ===");
        Console.WriteLine($"Тип: {_type}");
        Console.WriteLine($"Срок: {_durationMonths} месяцев");
        Console.WriteLine($"Цена за месяц: {_monthlyPrice} тг");
        Console.WriteLine($"ИТОГО: {CalculateTotal()} тг");
        Console.WriteLine($"Дата следующего продления: {DateTime.Now.AddMonths(_durationMonths):dd.MM.yyyy}");
    }
}

public class ServiceOrder : IOrder
{
    private DateTime _serviceDate;
    private string _status;
    private double _hourlyRate;
    private double _hours;
    private double _additionalFee;

    public ServiceOrder(DateTime serviceDate, double hourlyRate, double hours, double additionalFee = 0)
    {
        _serviceDate = serviceDate;
        _status = "Запланирована";
        _hourlyRate = hourlyRate;
        _hours = hours;
        _additionalFee = additionalFee;
    }

    public void Process() => Console.WriteLine("Подтверждаем оказание услуги...");

    public void ShowDetails() => Console.WriteLine($"Услуга | Дата: {_serviceDate:dd.MM.yyyy} | Статус: {_status}");

    public double CalculateTotal() => (_hourlyRate * _hours) + _additionalFee;

    public void GenerateInvoice()
    {
        Console.WriteLine("=== ИНВОЙС (Услуга) ===");
        Console.WriteLine($"Ставка за час: {_hourlyRate} тг");
        Console.WriteLine($"Количество часов: {_hours}");
        Console.WriteLine($"Доп. сборы: {_additionalFee} тг");
        Console.WriteLine($"ИТОГО: {CalculateTotal()} тг");
        Console.WriteLine($"Дата оказания: {_serviceDate:dd.MM.yyyy}");
        Console.WriteLine($"Текущий статус: {_status}");
    }
}

public static class OrderFactory
{
    public static IOrder CreateOrder(string type, params object[] args)
    {
        return type.ToLower() switch
        {
            "physical" => new PhysicalProductOrder(
                args[0].ToString(),
                Convert.ToDouble(args[1]),
                Convert.ToDouble(args[2])
            ),
            "digital" => new DigitalProductOrder(
                args[0].ToString(),
                args[1].ToString(),
                Convert.ToDouble(args[2]),
                Convert.ToDouble(args[3])
            ),
            "subscription" => new SubscriptionOrder(
                Convert.ToInt32(args[0]),
                args[1].ToString(),
                Convert.ToDouble(args[2])
            ),
            "service" => new ServiceOrder(
                (DateTime)args[0],
                Convert.ToDouble(args[1]),
                Convert.ToDouble(args[2]),
                args.Length > 3 ? Convert.ToDouble(args[3]) : 0
            ),
            _ => throw new ArgumentException("Неизвестный тип заказа")
        };
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Система создания заказов ===");
        Console.WriteLine();

        IOrder order1 = OrderFactory.CreateOrder("physical", "г. Алматы, ул. Сатпаева 22", 25000.0, 1500.0);
        IOrder order2 = OrderFactory.CreateOrder("digital", "LIC-123-ABC", "PDF", 8900.0, 1157.0);
        IOrder order3 = OrderFactory.CreateOrder("subscription", 6, "Премиум", 4500.0);
        IOrder order4 = OrderFactory.CreateOrder("service", DateTime.Now.AddDays(5), 5000.0, 3.5, 2000.0);

        var orders = new IOrder[] { order1, order2, order3, order4 };

        foreach (var order in orders)
        {
            order.ShowDetails();
            order.Process();
            Console.WriteLine($"Сумма заказа: {order.CalculateTotal()} тг");
            order.GenerateInvoice();
        }
    }
}