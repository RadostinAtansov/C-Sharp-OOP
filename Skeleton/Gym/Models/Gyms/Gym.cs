using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public class Gym : IGym
    {
        private string name;
        //private int capacity;
        private readonly List<IEquipment> equipment;
        private readonly List<IAthlete> athletes;

        public Gym(string name, int capacity)
        {
            equipment = new List<IEquipment>();
            athletes = new List<IAthlete>();
            this.Name = name;
            this.Capacity = capacity;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Gym name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Capacity { get; set; } // number of athletes exercise in Gym

        public double EquipmentWeight => this.equipment.Sum(a => a.Weight);

        public ICollection<IEquipment> Equipment => new List<IEquipment>(equipment);

        public ICollection<IAthlete> Athletes => new List<IAthlete>(athletes);

        public void AddAthlete(IAthlete athlete)
        {
            var count = this.Athletes.Count;
            var c = this.Capacity;

            if (count >= c)
            {
                throw new InvalidOperationException("Not enough space in the gym.");
            }
            this.athletes.Add(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            this.equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlet in this.Athletes)
            {
                athlet.Exercise();
            }
        }

        public virtual string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} is a {this.GetType().Name}:");
            string athlets = this.Athletes.Count > 0 ? string.Join(", ", this.Athletes) : "No athletes";
            sb.AppendLine($"Athletes: {athlets}");
            sb.AppendLine($"Equipment total count: {this.Equipment.Count}");
            sb.AppendLine($"Equipment total weight: {this.EquipmentWeight} grams");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return this.Athletes.Remove(athlete);
        }
    }
}
