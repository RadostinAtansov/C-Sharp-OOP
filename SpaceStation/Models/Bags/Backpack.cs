using SpaceStation.Models.Bags.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Bags
{
    public class Backpack : IBag
    {
        private ICollection<string> items;

        
        public Backpack()
        {
            this.items = new List<string>();
        }

        public ICollection<string> Items => new List<string>(items);


        public void AddItem(string item)
        {
            this.items.Add(item);
        }
    }
}
