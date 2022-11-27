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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }
        public User Traveler { get; set; }

        //Constructor with parameters
        public Travel(string destination, Countries country, int travellers, DateTime startDate, DateTime endDate, User traveler)
        {
            this.Destination = destination;
            this.Country = country;
            this.Travellers = travellers;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Traveler = traveler;
            this.TravelDays = CalculateTravelDays();
        }
       
        public virtual string GetInfo()
        {
            return $"{Country} with destination {Destination}}} | Travel Duration: {CalculateTravelDays()} days";
        }
        //Method that calculate the number of days between the date of departure and arrival
        private int CalculateTravelDays()
        {
            return Convert.ToInt32((EndDate - StartDate).Days);
        }
    }
}
