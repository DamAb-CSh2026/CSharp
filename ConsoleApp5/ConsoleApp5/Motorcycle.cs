using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Motorcycle : IVehicle
    {
        private string type;
        private int engineVolume;

        public Motorcycle(string type, int engineVolume)
        {
            this.type = type;
            this.engineVolume = engineVolume;
        }

        public void Drive()
        {
            Console.WriteLine($"Мотоцикл типа {type} едет.");
        }

        public void Refuel()
        {
            Console.WriteLine($"Мотоцикл заправляется. Объем двигателя: {engineVolume}");
        }
    }
}
