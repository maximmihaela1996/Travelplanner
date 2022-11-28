using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelpal.Enums;

namespace Travelpal.Models
{
    public class Travel
    {
        //all the properties of a travel
        public string Destination { get; set; }
        public Countries Country { get; set; }
        public int Travellers { get; set; }
        public User Traveler { get; set; }

        //Constructor with parameters
        public Travel(string destination, Countries country, int travellers, User traveler)
        {
            this.Destination = destination;
            this.Country = country;
            this.Travellers = travellers;
        }
        public virtual string GetInfo()
        {
            return $"{Country} with destination {Destination} ";
        }
    }
}
