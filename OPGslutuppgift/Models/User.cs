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
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

    }
}
