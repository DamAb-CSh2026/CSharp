using System;

public interface IPaymentStrategy
{
    void Pay(double amount);
}
public class CardPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} тенге банковской картой");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} тенге через PayPal");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} тенге криптовалютой");
    }
}

public class PaymentContext
{
    private IPaymentStrategy strategy;
    public void SetStrategy(IPaymentStrategy strategy)
    {
        this.strategy = strategy;
    }
    public void Pay(double amount)
    {
        strategy.Pay(amount);
    }
}

class Program
{
    staticvoid Main()
    {
        PaymentContext context = new PaymentContext();

        Console.WriteLine("Выберите способ оплаты: 1 - Карта, 2 - PayPal, 3 - Крипта");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
            context.SetStrategy(new CardPayment());
        else if (choice == 2)
            context.SetStrategy(new PayPalPayment());
        else
            context.SetStrategy(new CryptoPayment());

        context.Pay(1000);
    }
}
