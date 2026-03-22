using System;

public abstract class Beverage
{
    public abstract string GetDescription();
    public abstract int Cost();
}

public class Espresso : Beverage
{
    public override string GetDescription() => "Эспрессо";
    public override int Cost() => 800;
}

public class Tea : Beverage
{
    public override string GetDescription() => "Чай";
    public override int Cost() => 500;
}

public class Latte : Beverage
{
    public override string GetDescription() => "Латте";
    public override int Cost() => 1000;
}

public class Mocha : Beverage
{
    public override string GetDescription() => "Мокко";
    public override int Cost() => 1200;
}

public abstract class Addon : Beverage
{
    protected Beverage drink;

    public Addon(Beverage drink)
    {
        this.drink = drink;
    }
}

public class Milk : Addon
{
    public Milk(Beverage d) : base(d) { }

    public override string GetDescription() => drink.GetDescription() + ", молоко";
    public override int Cost() => drink.Cost() + 200;
}

public class Sugar : Addon
{
    public Sugar(Beverage d) : base(d) { }

    public override string GetDescription() => drink.GetDescription() + ", сахар";
    public override int Cost() => drink.Cost() + 50;
}

public class Cream : Addon
{
    public Cream(Beverage d) : base(d) { }

    public override string GetDescription() => drink.GetDescription() + ", сливки";
    public override int Cost() => drink.Cost() + 300;
}

public class Caramel : Addon
{
    public Caramel(Beverage d) : base(d) { }

    public override string GetDescription() => drink.GetDescription() + ", карамель";
    public override int Cost() => drink.Cost() + 250;
}

public class Chocolate : Addon
{
    public Chocolate(Beverage d) : base(d) { }

    public override string GetDescription() => drink.GetDescription() + ", шоколад";
    public override int Cost() => drink.Cost() + 300;
}

public class Program
{
    public static void Main()
    {
        Beverage b1 = new Espresso();
        b1 = new Milk(b1);
        b1 = new Sugar(b1);

        Console.WriteLine(b1.GetDescription());
        Console.WriteLine("Цена: " + b1.Cost());

        Console.WriteLine();

        Beverage b2 = new Latte();
        b2 = new Caramel(b2);
        b2 = new Cream(b2);

        Console.WriteLine(b2.GetDescription());
        Console.WriteLine("Цена: " + b2.Cost());

        Console.WriteLine();

        Beverage b3 = new Mocha();
        b3 = new Chocolate(b3);
        b3 = new Milk(b3);
        b3 = new Sugar(b3);

        Console.WriteLine(b3.GetDescription());
        Console.WriteLine("Цена: " + b3.Cost());
    }
}