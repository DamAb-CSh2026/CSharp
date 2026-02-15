using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class BusFactory : VehicleFactory
    {
        private int passengers;

        public BusFactory(int passengers)
        {
            this.passengers = passengers;
        }

        public override IVehicle CreateVehicle()
        {
            return new Bus(passengers);
        }
    }
}
