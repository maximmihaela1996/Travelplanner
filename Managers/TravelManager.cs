using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Travelpal.Interfaces;
using Travelpal.Models;

namespace Travelpal.Managers
{
    public class TravelManager
    {
        public static List<IUser> UsersList = new();
        public static List<Travel> Travels { get; set; } = new();
        private UserManager userManager;
        private IUser SignedIn;
        public List<string> TravelTypes { get; set; } = new() { "Trip", "Vacation" };
        public TravelManager(UserManager userManager, IUser SignedIn)
        {
            this.SignedIn = SignedIn;
            this.userManager = userManager;
        }
        public void AddNewTravel(Travel travel)
        {
            if (SignedIn is User)
            {
                ((User)SignedIn).travels.Add(travel);
            }
        }
        public bool DeleteTravel(Travel travelToRemove)
        {
            Travels.Remove(travelToRemove);
            return true;
        }
    }

}
