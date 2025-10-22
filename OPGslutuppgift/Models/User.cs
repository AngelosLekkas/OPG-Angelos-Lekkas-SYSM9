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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }


        //metoder
        public void ValidateLogin() //validerar att password stämmer överens.
        {
            
        }

        public void ChangePassword() //sätter Password till newPassword input.
        {
            
        }

        public void UpdateDetails() //sätter username och country till nya inputs.
        {

        }
    }
}
