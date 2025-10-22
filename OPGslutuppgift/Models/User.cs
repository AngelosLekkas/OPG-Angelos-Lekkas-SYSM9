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
        public virtual bool ValidateLogin(string inputPassword) //validerar att password stämmer överens.
        {
            return Password == inputPassword;
        }

        public virtual void ChangePassword(string newPassword) //sätter Password till newPassword input.
        {
            Password = newPassword;
        }

        public void UpdateDetails(string newUsername, string newCountry) //sätter username och country till nya inputs.
        {
            Username = newUsername;
            Country = newCountry;
        }
    }
}
