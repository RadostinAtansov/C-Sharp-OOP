﻿using MilitaryElite.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {

        private ICollection<ISoldier> privates;
        private object stringbuilder;

        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary)
            : base(id, firstName, lastName, salary)
        {
            this.privates = new List<ISoldier>();
        }

        public IReadOnlyCollection<ISoldier> Privates => 
            (IReadOnlyCollection<ISoldier>)this.privates;

        public void AddPrivate(ISoldier @private)
        {
            this.privates.Add(@private);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                .AppendLine("Privates:");

            foreach (var soldier in this.privates)
            {
                sb.AppendLine(soldier.ToString());
            }


            return sb.ToString().TrimEnd();
        }
    }
}
