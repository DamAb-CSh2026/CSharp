using System;
using System.Collections.Generic;

namespace VehicleFactorySimple
{
    // 1. Интерфейс
    public interface IVehicle
    {
        void Drive();
        void Refuel();
        void ShowInfo();
    }

    // 2. Классы транспорта
    public class Car : IVehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        public Car(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        public void Drive() => Console.WriteLine($"Авто {Brand} {Model} едет");
        public void Refuel() => Console.WriteLine($"Авто {Brand} {Model} заправляется");
        public void ShowInfo() => Console.WriteLine($"Авто: {Brand} {Model}");
    }

    public class Motorcycle : IVehicle
    {
        public string Type { get; set; }
        public int EngineVolume { get; set; }

        public Motorcycle(string type, int volume)
        {
            Type = type;
            EngineVolume = volume;
        }

        public void Drive() => Console.WriteLine($"Мотоцикл {Type} едет");
        public void Refuel() => Console.WriteLine("Мотоцикл заправляется");
        public void ShowInfo() => Console.WriteLine($"Мотоцикл: {Type}, {EngineVolume}cc");
    }

    public class Truck : IVehicle
    {
        public double Capacity { get; set; }

        public Truck(double capacity) => Capacity = capacity;

        public void Drive() => Console.WriteLine($"Грузовик на {Capacity}т едет");
        public void Refuel() => Console.WriteLine("Грузовик заправляется");
        public void ShowInfo() => Console.WriteLine($"Грузовик: {Capacity}т");
    }

    // Новый тип - Автобус
    public class Bus : IVehicle
    {
        public int Seats { get; set; }
        public Bus(int seats) => Seats = seats;

        public void Drive() => Console.WriteLine("Автобус едет");
        public void Refuel() => Console.WriteLine("Автобус заправляется");
        public void ShowInfo() => Console.WriteLine($"Автобус: {Seats} мест");
    }

    // 3. Абстрактная фабрика
    public abstract class VehicleFactory
    {
        public abstract IVehicle Create();
    }

    // 4. Конкретные фабрики
    public class CarFactory : VehicleFactory
    {
        string _brand, _model;
        public CarFactory(string brand, string model) { _brand = brand; _model = model; }
        public override IVehicle Create() => new Car(_brand, _model);
    }

    public class MotoFactory : VehicleFactory
    {
        string _type; int _volume;
        public MotoFactory(string type, int volume) { _type = type; _volume = volume; }
        public override IVehicle Create() => new Motorcycle(_type, _volume);
    }

    public class TruckFactory : VehicleFactory
    {
        double _capacity;
        public TruckFactory(double capacity) { _capacity = capacity; }
        public override IVehicle Create() => new Truck(_capacity);
    }

    public class BusFactory : VehicleFactory
    {
        int _seats;
        public BusFactory(int seats) { _seats = seats; }
        public override IVehicle Create() => new Bus(_seats);
    }

    // 5. Главная программа
    class Program
    {
        static List<IVehicle> vehicles = new List<IVehicle>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n1.Авто 2.Мото 3.Грузовик 4.Автобус 5.Показать все 6.Тест 0.Выход");
                Console.Write("Выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": CreateCar(); break;
                    case "2": CreateMoto(); break;
                    case "3": CreateTruck(); break;
                    case "4": CreateBus(); break;
                    case "5": ShowAll(); break;
                    case "6": TestAll(); break;
                    case "0": return;
                }
            }
        }

        static void CreateCar()
        {
            Console.Write("Марка: "); string brand = Console.ReadLine();
            Console.Write("Модель: "); string model = Console.ReadLine();
            vehicles.Add(new CarFactory(brand, model).Create());
            Console.WriteLine("Авто создано!");
        }

        static void CreateMoto()
        {
            Console.Write("Тип: "); string type = Console.ReadLine();
            Console.Write("Объем: "); int vol = int.Parse(Console.ReadLine());
            vehicles.Add(new MotoFactory(type, vol).Create());
            Console.WriteLine("Мото создано!");
        }

        static void CreateTruck()
        {
            Console.Write("Грузоподъемность (т): "); double cap = double.Parse(Console.ReadLine());
            vehicles.Add(new TruckFactory(cap).Create());
            Console.WriteLine("Грузовик создан!");
        }

        static void CreateBus()
        {
            Console.Write("Количество мест: "); int seats = int.Parse(Console.ReadLine());
            vehicles.Add(new BusFactory(seats).Create());
            Console.WriteLine("Автобус создан!");
        }

        static void ShowAll()
        {
            Console.WriteLine("\n=== Весь транспорт ===");
            foreach (var v in vehicles) v.ShowInfo();
        }

        static void TestAll()
        {
            Console.WriteLine("\n=== Тест ===");
            foreach (var v in vehicles)
            {
                v.Drive();
                v.Refuel();
                Console.WriteLine();
            }
        }
    }
}