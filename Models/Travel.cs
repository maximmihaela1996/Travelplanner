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
        public int Id { get;}
        //All the properties of a travel
        public string Destination { get; set; }
        public Countries Country { get; set; }
        public int Travellers { get; set; }
        public User Traveler { get; set; }

        private static int LastId = 1; 

        //Constructor with parameters
        public Travel(string destination, Countries country, int travellers, User traveler)
        {
            this.Id = LastId++;
            this.Destination = destination;
            this.Country = country;
            this.Travellers = travellers;
        }
        public virtual string GetInfo()
        {
            return $"From: {Country} --> Destination: {Destination} ";
        }
    }
}
