using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Travelpal.Interfaces;
using Travelpal.Models;

namespace Travelpal.Managers
{
    public class TravelManager
    {
        //Declaration of UsersList, TravelsList Usermanager and IUser object
        public static List<IUser> UsersList = new();
        public List<Travel> Travels = new();
        private UserManager userManager;
        private IUser SignedIn;

        //List of TravelTypes
        public List<string> TravelTypes { get; set; } = new() { "Trip", "Vacation" };
        public TravelManager(UserManager userManager, IUser SignedIn)
        {
            this.SignedIn = SignedIn;
            this.userManager = userManager;
        }

        //Method that adds a new trevel to the user's travel list
        public void AddNewTravel(Travel travel)
        {
            if (SignedIn is User)
            {
                ((User)SignedIn).travels.Add(travel);
            }
        }
        // Method that remove a travel as Admin
        public bool DeleteTravel(Travel travelToRemove)
        {
            //Travels(travelToRemove);
            //go through each entity from the list of users
            foreach (var user in UserManager.UsersList)
            {
                  if (user.GetType() == typeof(User))
                  {
                    //go through all the users travels
                    foreach (Travel travel in ((User)user).travels)
                      {
                        //if  selected travel has the same id as a travel from the list -> the travel is removed
                          if (travel.Id == travelToRemove.Id)
                          {
                            ((User)user).travels.Remove(travelToRemove);
                              return true;
                          }
                      }
                  }
            }
                      return false;     
        }

        //Method that remove a travel as User
        public bool DeleteTravel(Travel travelToRemove, IUser SignedIn)
        {
            //Goes through all the travel that the current user has
            foreach (Travel travel in ((User)SignedIn).travels)
            {
                //If a travel from the list had the same id as the selected traved
                if (travel.Id == travelToRemove.Id)
                {
                    //Delete travel
                    ((User)SignedIn).travels.Remove(travelToRemove);
                    return true;
                }
            }
            return false;
        }
    }

}
