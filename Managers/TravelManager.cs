using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelpal.Interfaces;
using Travelpal.Models;

namespace Travelpal.Managers
{
    public class TravelManager
    {
        public List<string> TravelTypes { get; set; } = new() { "Trip", "Vacation" };
        public TravelManager() { }

        // clasa de servicii = scop: prelucrari

        public void AddNewTravel(Travel travel)
        {
            
        }

    }
}
