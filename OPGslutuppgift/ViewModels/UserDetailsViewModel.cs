using OPGslutuppgift.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPGslutuppgift.ViewModels
{
    public class UserDetailsViewModel
    {
        //props
        public UserManager UserManager { get; }

        //konstruktor
        public UserDetailsViewModel(UserManager userManager)
        {
            UserManager = userManager;
        }
    }
}
