using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelpal.Models;

namespace Travelpal.Managers
{
    public class TravelManager
    {
        private List<Travel> travels = new();

        // Adding sent Travel object to list of travels
        public void AddTravel(Travel travel)
        {
            travels.Add(travel);
        }
        // Removing sent Travel object from list of travels
        public void RemoveTravel(Travel travel)
        {
            travels.Remove(travel);
        }
        // Method for accessing the private list of travels
        public List<Travel> GetAllTravels()
        {
            return travels;
        }
    }
}
