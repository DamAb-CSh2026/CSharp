using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Bus : IVehicle
    {
        private int passengers;

        public Bus(int passengers)
        {
            this.passengers = passengers;
        }

        public void Drive()
        {
            Console.WriteLine($"Автобус едет. Пассажиров: {passengers}");
        }

        public void Refuel()
        {
            Console.WriteLine("Автобус заправляется.");
        }
    }
}
