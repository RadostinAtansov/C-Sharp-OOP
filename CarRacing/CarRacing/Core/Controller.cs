using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Repositories.Contracts;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private readonly IRepository<ICar> cars;
        private readonly IRepository<IRacer> racers;
        private readonly IMap map;

        public Controller()
        {
            cars = new CarRepository();
            racers = new RacerRepository();
            map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            string result;

            if (type == "SuperCar")
            {
                Car car = new SuperCar(make, model, VIN, horsePower);
                this.cars.Add(car);
                result =$"Successfully added car {make} {model} ({VIN}).";
            }
            else if(type == "TunedCar")
            {
                Car car = new TunedCar(make, model, VIN, horsePower);
                this.cars.Add(car);
                result = $"Successfully added car {make} {model} ({VIN}).";
            }
            else
            {
                throw new ArgumentException("Invalid car type!");
            }
            return result;
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            var carRacer = this.cars.FindBy(carVIN);

            string result;

            if (carRacer == null)
            {
                throw new ArgumentException("Car cannot be found");
            }
            if (type == "ProfessionalRacer")
            {
                IRacer racer = new ProfessionalRacer(username, carRacer);
                this.racers.Add(racer);
                result = $"Successfully added racer {username}.";
            }
            else if (type == "StreetRacer")
            {
                IRacer racer = new ProfessionalRacer(username, carRacer);
                this.racers.Add(racer);
                result = $"Successfully added racer {username}.";
            }
            else
            {
                throw new ArgumentException("Invalid racer type");
            }
            return result;
        }
    

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            var racerOne = this.racers.FindBy(racerOneUsername);
            var racerTwo = this.racers.FindBy(racerTwoUsername);

            string result;

            if (racerOne == null)
            {
                throw new ArgumentException($"Racer {racerOneUsername} cannot be found!");
            }
            else if (racerTwo == null)
            {
                throw new ArgumentException($"Racer {racerTwoUsername} cannot be found!");
            }
            result = map.StartRace(racerOne, racerTwo);

            return result;
        }

        public string Report()
        {
            return racers.ToString();
        }
    }
}
