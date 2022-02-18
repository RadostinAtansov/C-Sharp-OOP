using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;
        private Backpack bag;

        public Astronaut(string name, double oxygen)
        {
            this.Oxygen = oxygen;
            this.Name = name;
            bag = new Backpack();
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Astronaut name cannot be null or empty.");
                }
                name = value;
            }
        }

        public double Oxygen
        {
            get
            {
                return oxygen;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Cannot create Astronaut with negative oxygen!");
                }
                oxygen = value;
            }
        }

        public bool CanBreath { get; set; }

        public IBag Bag => new Backpack();

        public virtual void AddItemAstro(string item)
        {
            this.Bag.Items.Add(item);
        }

        public virtual void Breath()
        {
            if (this.Oxygen - 10 < 0)
            {
                this.Oxygen = 0;
            }
            else
            {
                this.Oxygen -= 10;
            }
        }
    }
}
