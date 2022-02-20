using CarRacing.Models.Cars.Contracts;
using System;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        private string make;
        private string model;
        private string vin;
        private int horsePower;
        private double fuelAvailable;
        private double fuelConsumptionPerRace;

        public Car(string make, string model, string vin, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            this.VIN = vin;
            this.Make = make;
            this.Model = model;
            this.HorsePower = horsePower;
            this.FuelAvailable = fuelAvailable;
            this.FuelConsumptionPerRace = fuelConsumptionPerRace;
        }

        public string Make 
        { 
            get
            {
                return make;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car make cannot be null or empty.");
                }
                make = value;
            }
        }

        public string Model 
        { 
            get
            {
                return model;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car model cannot be null or empty.");
                }
                model = value;
            }
        }

        public string VIN 
        { 
            get
            {
                return vin;
            }
            set
            {
                if (value.Length != 17)
                {
                    throw new ArgumentException("Car VIN must be exactly 17 characters long.");
                }
                vin = value;
            }
        }

        public int HorsePower 
        { 
            get
            {
                return horsePower;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Horse power cannot be below 0.");
                }
                horsePower = value;
            }
        }

        public double FuelAvailable 
        { 
            get
            {
                return fuelAvailable;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                fuelAvailable = value;
            }
        }

        public double FuelConsumptionPerRace 
        { 
            get
            {
                return fuelConsumptionPerRace;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel consumption cannot be below 0.");
                }
                fuelConsumptionPerRace = value;
            }
        }

        public void Drive()
        {
            if (this.GetType().Name == "TunedCar")
            {
                this.horsePower = this.horsePower * (int)0.97;
            }
            this.fuelAvailable -= this.fuelConsumptionPerRace;
        }
    }
}
