using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPGslutuppgift.Models
{
    public class User
    {
        //props, User ska ha användarnamn, password & land.
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Country { get; private set; }

        //konstruktor
        public User(string username, string password, string country)
        {
            Username = username;
            Password = password;
            Country = country;
        }

        //metoder
    }
}
