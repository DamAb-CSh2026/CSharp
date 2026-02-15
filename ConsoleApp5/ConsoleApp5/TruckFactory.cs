using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class TruckFactory : VehicleFactory
    {
        private int capacity;
        private int axles;

        public TruckFactory(int capacity, int axles)
        {
            this.capacity = capacity;
            this.axles = axles;
        }

        public override IVehicle CreateVehicle()
        {
            return new Truck(capacity, axles);
        }
    }
}
