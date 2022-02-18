using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private ICollection<IAstronaut> spaceStationModel;

        public AstronautRepository()
        {
            spaceStationModel = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => new List<IAstronaut>(spaceStationModel);

        public void Add(IAstronaut model)
        {
            this.spaceStationModel.Add(model);
        }

        public IAstronaut FindByName(string name)
        {
            var astr = spaceStationModel.FirstOrDefault(a => a.Name == name);
            if (astr != null)
            {
                return astr;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IAstronaut model)
        {
            return this.spaceStationModel.Remove(model);
        }
    }
}
