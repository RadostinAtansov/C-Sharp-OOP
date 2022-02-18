using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{

    public class Controller : IController
    {
        IRepository<IAstronaut> astronautRepository;
        IRepository<IPlanet> planetRepository;

        public Controller()
        {
            astronautRepository = new AstronautRepository();
            planetRepository = new PlanetRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            string result = string.Empty;

            if (type == nameof(Biologist))
            {
                IAstronaut bio = new Biologist(astronautName);
                astronautRepository.Add(bio);
                result = $"Successfully added {type}: {astronautName}!";
            }
            else if (type == nameof(Geodesist))
            {
                IAstronaut geo = new Geodesist(astronautName);
                astronautRepository.Add(geo);
                result = $"Successfully added {type}: {astronautName}!";
            }
            else if (type == nameof(Meteorologist))
            {
                IAstronaut meteo = new Meteorologist(astronautName);
                astronautRepository.Add(meteo);
                result = $"Successfully added {type}: {astronautName}!";
            }
            else
            {
                throw new InvalidOperationException("Astronaut type doesn't exists!");
            }

            return result;
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            Planet planet = new Planet(planetName);

            for (int i = 0; i < items.Length; i++)
            {
                planet.AddItem(items[i]);

            }
                //planet.Items2.Add(item);

            
            planetRepository.Add(planet);

            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> astronauts = (List<IAstronaut>)astronautRepository.Models;
            //List<IAstronaut> astroWithOxygenMoreThan60units = (List<IAstronaut>)collection.Select(a => a.Oxygen > 60);

            List<IAstronaut> astroWithOxygenMoreThan60units = new List<IAstronaut>();
            foreach (var item in astronauts)
            {
                if (item.Oxygen > 60)
                {
                    astroWithOxygenMoreThan60units.Add(item);
                }
            }

            var planet = planetRepository.Models.FirstOrDefault(p => p.Name == planetName);

            if (astroWithOxygenMoreThan60units.Count() > 0)
            {
                Mission mission = new Mission();
                mission.Explore(planet, astroWithOxygenMoreThan60units);
            }
            else
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet");
            }

            var deadAstronauts = astroWithOxygenMoreThan60units.Where(da => da.Oxygen == 0);
            return $"Planet: {planetName} was explored! Exploration finished with {deadAstronauts.Count()} dead astronauts!";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{planetRepository.Models.Count} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (var astro in astronautRepository.Models)
            {
                var bagItems = astro.Bag.Items.Count() > 0 ? string.Join(", ", astro.Bag.Items) : "None";
                var bag = astro.Bag.Items.Count > 0 ? string.Join(", ", bagItems) : "None";

                sb.AppendLine($"Name: {astro.Name}");
                sb.AppendLine($"Oxygen: {astro.Oxygen}");
                sb.AppendLine($"Bag items: {bagItems}");
            }

            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            var astro = astronautRepository.FindByName(astronautName);

            if (astro == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} was retired!");
            }
            else
            {
                astronautRepository.Remove(astro);
            }
            return $"Astronaut {astronautName} was retired!";
        }
    }
}
