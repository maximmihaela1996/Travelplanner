using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelpal.Enums;

namespace Travelpal.Models
{
    public class Trip : Travel
    {
        public TripTypes TripType { get; set; }
        public Trip(string destination, Countries country, int travellers, DateTime startDate, DateTime endingDate, TripTypes tripType, User traveler) 
                   : base(destination, country, travellers, startDate, endingDate, traveler)
        {
            this.TripType = tripType;
        }
        public override string GetInfo()
        {
            return base.GetInfo();
        }
    }
}
