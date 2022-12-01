
using System.Collections.Generic;

using Travelpal.Enums;
using Travelpal.Interfaces;
using Travelpal.Models;

namespace Travelpal.Managers
{
    public class UserManager
    {
        //Create a list of IUser (with user and admin type objects);
        public static List<IUser> UsersList = new();
        public static List<Travel> Travels = new();
        public IUser SignedIn;
        public UserManager()
        {
            //Calling the create method which  create new objects of types User and Admin and objects of type travel(Trip, Vacation)
            if (UsersList.Count == 0)
            {
                CreateDefaultUsers();
            }
        }
        // Method that create Default users and travels
        public void CreateDefaultUsers()
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
            Vacation defaultVacation = new("Alicante", Countries.Spain, 2, true, gandalf);
         //Add the new object (travel) into the travels list
            gandalf.travels.Add(defaultVacation);


         //Create new travel of type Trip and assigned it with an user (gandalf)
            Trip defaultTrip = new("Belgrad", Countries.Serbia, 1, TripTypes.Work, gandalf);
         //Add the new object (trip) into the travels list
            gandalf.travels.Add(defaultTrip);
        }

        //Method that loops through the Userslist and returns all users from the list  --> Called in MainWindow when the user SignUp
        public List<IUser> GetAllUsers()
        {
            return UsersList;
        }

        //Method that return if username and password has a specific size 
        public bool ValidateNewUser(string username, string password)
        {
            if (username.Length > 3 && password.Length > 5)
            {
                return true;
            }else
            {
                return false;
            }
        }
        //Method that return if the user already exists
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
                    //Add newUser in the UsersList
                    UsersList.Add(newUser);
                    return true;
                }
            }
            return false;
        }
        // Method that updates the user
        public bool UpdateUser(User userToUpdate)
        {
            //Goes through the entire list of users
            for (int i = 0; i < UsersList.Count; i++)
            {
                //If the user is of the User type
                if (UsersList[i] is User) {
                    //Check each user in the list if it has the same Id with the user selected for update
                    if (((User)UsersList[i]).UserId == userToUpdate.UserId)
                    {
                        //Update the password and username
                        UsersList[i].Username = userToUpdate.Username;
                        UsersList[i].Password = userToUpdate.Password;
                        UsersList[i].Location = userToUpdate.Location;

                        return true;
                    }
                }
             }
            return false;
        }
    }
}
