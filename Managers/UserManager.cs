using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Travelpal.Enums;
using Travelpal.Interfaces;
using Travelpal.Models;

namespace Travelpal.Managers
{
    public class UserManager
    {
        //Create a list of IUser (with user and admin type objects);
        private List<IUser> UsersList = new();
        public IUser SignedIn;
        public UserManager()
        {
         //Calling the create method which  create new objects of types User and Admin and objects of type travel(Trip, Vacation)
            CreateDefaultUsers();
        }
        // Method that create Default users and travels
        private void CreateDefaultUsers()
        {
         //Create new user of type Admin
            Admin admin = new("admin", "password", Countries.Sweden);
         //Add the new object (admin) in the list of users
            UsersList.Add(admin);

         //Create new user of type User
            User gandalf = new("Gandalf", "password", Countries.Bahamas);
         //Add the new object (user) in the list of users
            UsersList.Add(gandalf);

         //Create new travel of type Vacation and assigned it with an user (gandalf)
            Vacation defaultVacation = new("Barcelona", Countries.Spain, 2, new DateTime(2023, 02, 01), new DateTime(2023, 02, 15), true, gandalf);
         //Add the new object (travel) into the travels list
            gandalf.UsersTravels.Add(defaultVacation);

         //Create new travel of type Trip and assigned it with an user (gandalf)
            Trip defaultTrip = new("Gdansk", Countries.Poland, 1, new DateTime(2022, 12, 23), new DateTime(2023, 12, 27), TripTypes.Leisure, gandalf);
         //Add the new object (trip) into the travels list
            gandalf.UsersTravels.Add(defaultTrip);
        }

        //Method that loops through the list and returns all the elements from the list
        public List<IUser> GetAllUsers()
        {
            return UsersList;
        }
        //Method that goes through all the entities of the user's list and returns true or false if a user is found or not
        public bool SignInAsUserOrAdmin(string username, string password)
        {
            foreach (IUser user in UsersList)
            {
                //Check if the variables from the inputs matching with any in the list
                if (user.Username == username && user.Password == password)
                {
                    //If yes, the object SignedIn is assigned the value of the object which was found in the list
                    SignedIn = user;
                    //A user of type admin or user was found in the list - return true
                    return true;
                }
             }
            //No users who matching the parameters(username, password) was found - return false
            return false;
        }

        //Method that verify if the current user is of type User 
        public bool SignInAsUser()
        {
            if(SignedIn is User)
            {
                return true;
            }
            return false;
        }
        //Method that return if username and password has a specific size and if the user is already existing
        public bool ValidateNewUser(string username, string password)
        {
            if (username.Length > 3 && password.Length > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Method that return if an user is allready in the list
        public bool ValidateUserExisting(string username, string password)
        {
            foreach (IUser user in UsersList)
            {
                if (user.Username == username && user.Password == password)
                {
                    return false;
                }
            }
            return true;
        }
        //Method that added a new user if the ValidateNewUser and ValidateUserExisting return true
        public bool AddNewUser(IUser newUser)
        {
            if (ValidateNewUser(newUser.Username, newUser.Password))
            {
                if (ValidateUserExisting(newUser.Username, newUser.Password))
                {
                    UsersList.Add(newUser);
                    return true;
                }
            }
            return false;
        }
        
    }
}
