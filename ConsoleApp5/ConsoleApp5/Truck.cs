using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Truck : IVehicle
    {
        private int loadCapacity;
        private int axles;

        public Truck(int loadCapacity, int axles)
        {
            this.loadCapacity = loadCapacity;
            this.axles = axles;
        }

        public void Drive()
        {
            Console.WriteLine($"Грузовик едет. Грузоподъемность: {loadCapacity}");
        }

        public void Refuel()
        {
            Console.WriteLine($"Грузовик заправляется. Количество осей: {axles}");
        }
    }
}
