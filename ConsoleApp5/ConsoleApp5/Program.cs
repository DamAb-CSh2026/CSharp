using System;
using ConsoleApp5;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите тип транспорта: car / motorcycle / truck / bus");
        string type = Console.ReadLine().ToLower();

        VehicleFactory factory = null;

        switch (type)
        {
            case "car":
                Console.Write("Марка: ");
                string brand = Console.ReadLine();

                Console.Write("Модель: ");
                string model = Console.ReadLine();

                Console.Write("Тип топлива: ");
                string fuel = Console.ReadLine();

                factory = new CarFactory(brand, model, fuel);
                break;

            case "motorcycle":
                Console.Write("Тип мотоцикла: ");
                string mType = Console.ReadLine();

                Console.Write("Объем двигателя: ");
                int volume = int.Parse(Console.ReadLine());

                factory = new MotorcycleFactory(mType, volume);
                break;

            case "truck":
                Console.Write("Грузоподъемность: ");
                int cap = int.Parse(Console.ReadLine());

                Console.Write("Количество осей: ");
                int ax = int.Parse(Console.ReadLine());

                factory = new TruckFactory(cap, ax);
                break;

            case "bus":
                Console.Write("Количество пассажиров: ");
                int pass = int.Parse(Console.ReadLine());

                factory = new BusFactory(pass);
                break;

            default:
                Console.WriteLine("Неизвестный тип транспорта");
                return;
        }

        IVehicle vehicle = factory.CreateVehicle();

        vehicle.Drive();
        vehicle.Refuel();
    }
}
