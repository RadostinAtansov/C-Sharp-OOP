using System.Linq;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            //var withoutOxygen = astronauts.Where(o => o.Oxygen <= 0);
            //var withOxygen = astronauts.Where(o => o.Oxygen > 0);

            //List<IAstronaut> withOxygen = new List<IAstronaut>();

            //foreach (var astro in astronauts)
            //{
            //    if (astro.Oxygen > 0)
            //    {
            //        withOxygen.Add(astro);
            //    }
            //}

            foreach (var astr in astronauts)
            {
                if (planet.Items.Count > 0)
                {
                    foreach (var item in planet.Items)
                    {
                        if (astr.Oxygen == 0)
                        {
                            break;
                        }
                        astr.Bag.Items.Add(item);
                        astr.Breath();
                        planet.Items.Remove(item);
                    }
                }
            }
        }
    }
}
