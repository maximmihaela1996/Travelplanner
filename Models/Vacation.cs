using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelpal.Enums;

namespace Travelpal.Models
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }
        public Vacation(string destination, Countries country, int travellers, DateTime startDate, DateTime endDate, bool allInclusive, User traveler) 
            : base(destination, country, travellers, startDate, endDate, traveler)
        {
            this.AllInclusive = allInclusive;
        }
        public override string GetInfo()
        {
            return base.GetInfo();
        }

    }
}
