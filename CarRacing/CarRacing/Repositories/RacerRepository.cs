using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private ICollection<IRacer> models;

        public RacerRepository()
        {
            models = new List<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models => new List<IRacer>();

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Racer Repository");
            }
            this.models.Add(model);
        }

        public IRacer FindBy(string property)
        {
           return this.models.FirstOrDefault(r => r.Username == property);
        }

        public bool Remove(IRacer model)
        {
            return this.models.Remove(model);
        }

        public override string ToString()
        {
            var orderList = this.models
                .OrderByDescending(a => a.DrivingExperience)
                .ThenBy(u => u.Username);

            StringBuilder sb = new StringBuilder();

            foreach (var racer in orderList)
            {
                sb.AppendLine($"{racer.GetType().Name}: {racer.Username}");
                sb.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                sb.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                sb.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
