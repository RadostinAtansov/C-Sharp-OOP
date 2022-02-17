using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {

        private IRepository<IEquipment> equipments;
        private ICollection<IGym> gyms;

        public Controller()
        {
            equipments = new Repository<Equipment>();
            gyms = new List<IGym>();
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            var gym = this.gyms.FirstOrDefault(g => g.Name == gymName);

            string result = string.Empty;

            if (athleteType == "Boxer" && gym.Name == gymName && gym.GetType().Name == "BoxingGym")
            {
                IAthlete athlete = new Boxer(athleteName, motivation, numberOfMedals);
                gym.AddAthlete(athlete);
                result = $"Successfully added {athleteType} to {gymName}.";
            }
            else if (athleteType == "Weightlifter" && gym.Name == gymName && gym.GetType().Name == "WeightliftingGym")
            {
                IAthlete athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
                gym.AddAthlete(athlete);
                result = $"Successfully added {athleteType} to {gymName}.";
            }
            else if (string.IsNullOrWhiteSpace(athleteType) || athleteType != "Weightlifter" || athleteType != "Boxer")
            {
                throw new InvalidOperationException("Invalid athlete type");
            }
            else
            {
                result = "The gym is not appropriate.";
            }
            return result;
        }

        public string AddEquipment(string equipmentType)
        {
            //var equipment = this.equipments.FindByType(equipmentType);
            string result = string.Empty;

            if (equipmentType == "Kettlebell")
            {
                Kettlebell createEquiment = new Kettlebell();
                this.equipments.Add(createEquiment);
                result = $"Successfully added {equipmentType}.";
            }
            else if (equipmentType == "BoxingGloves")
            {
                BoxingGloves createEquiment = new BoxingGloves();
                this.equipments.Add(createEquiment);
                result = $"Successfully added {equipmentType}.";
            }
            else
            {
                throw new InvalidOperationException("Invalid equipment type.");
            }
            return result;
        }

        public string AddGym(string gymType, string gymName)
        {
            var gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            string result = string.Empty;

            if (gymType == "BoxingGym")
            {
                this.gyms.Add(new BoxingGym(gymName));
                result = $"Successfully added {gymType}.";
            }
            else if (gymType == "WeightliftingGym")
            {
                this.gyms.Add(new WeightliftingGym(gymName));
                result = $"Successfully added {gymType}.";
            }
            else
            {
                throw new InvalidOperationException("Invalid gym type.");
            }

            return result;
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = this.gyms.FirstOrDefault(g => g.Name == gymName);

            var sumGymWeight = gym.Equipment.Sum(w => w.Weight);

            return $"The total weight of the equipment in the gym {gymName} is {sumGymWeight:d2} grams.";
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            var equipment = this.equipments.FindByType(equipmentType);
            string result = string.Empty;

            if (equipment != null && gym != null)
            {
                this.equipments.Remove(equipment);
                gym.Equipment.Add(equipment);
                gym.AddEquipment(equipment);
                result = $"Successfully added {equipmentType} to {gymName}.";
            }
            else if (equipment == null)
            {
                throw new InvalidOperationException($"There isn’t equipment of type {equipmentType}.");
            }
            return result;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IGym gym in this.gyms)
            {

                var gymAthlets = gym.Athletes.Count > 0 ? /*$"{gym.Athletes.SelectMany(a => a.FullName)}*/string.Join(", ", gym.Athletes.Select(a => a.FullName)) : "No Athletss";
                sb.AppendLine($"{gym.Name} is a {gym.GetType().Name}");
                sb.AppendLine($"Athletes: {gymAthlets}");
                sb.AppendLine($"Equipment total count: {gym.Equipment.Count}");
                sb.AppendLine($"Equipment total weight: {gym.EquipmentWeight:f2} grams");
            }

             return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            var gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            gym.Exercise();
            return $"Exercise athletes: {gym.Athletes.Count()}.";
        }
    }
}
