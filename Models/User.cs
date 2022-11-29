using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelpal.Enums;
using Travelpal.Interfaces;

namespace Travelpal.Models
{
    public class User : IUser
    {
        public int UserId { get;} 
        public string Username { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }
        public List<Travel> travels { get; set; } = new();

        private int LastId = 1; // 0 is Admin

        public User(string username, string password, Countries location)
        {
            this.UserId = LastId++;
            //LastId = LastId + 1;
            Username = username;
            Password = password;
            Location = location;
        }
    }
}
