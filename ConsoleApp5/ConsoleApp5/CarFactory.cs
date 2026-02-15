using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class CarFactory : VehicleFactory
    {
        private string brand;
        private string model;
        private string fuel;

        public CarFactory(string brand, string model, string fuel)
        {
            this.brand = brand;
            this.model = model;
            this.fuel = fuel;
        }

        public override IVehicle CreateVehicle()
        {
            return new Car(brand, model, fuel);
        }
    }
}
