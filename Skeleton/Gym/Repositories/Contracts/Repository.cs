using Gym.Models.Equipment.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories.Contracts
{
    public class Repository<IEquipmentT> : IRepository<IEquipment>
    {
        private readonly ICollection<IEquipment> equipments;

        public Repository()
        {
            equipments = new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => new List<IEquipment>(equipments);

        public void Add(IEquipment model)
        {
            this.equipments.Add(model);
        }

        public IEquipment FindByType(string type)
        {
            return this.equipments.FirstOrDefault(eq => eq.GetType().Name == type);
        }

        public bool Remove(IEquipment model)
        {
            return this.equipments.Remove(model);
        }
    }
}
