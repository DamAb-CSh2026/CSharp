using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class MotorcycleFactory : VehicleFactory
    {
        private string type;
        private int volume;

        public MotorcycleFactory(string type, int volume)
        {
            this.type = type;
            this.volume = volume;
        }

        public override IVehicle CreateVehicle()
        {
            return new Motorcycle(type, volume);
        }
    }
}
