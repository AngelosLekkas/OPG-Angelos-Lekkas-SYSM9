using OPGslutuppgift.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPGslutuppgift.ViewModels
{
    public class MainViewModel
    {
        //skapa usermanager prop
        public UserManager UserManager { get; }

        //hämta usermanager via konstruktorn
        public MainViewModel(UserManager userManager)
        {
            UserManager = userManager;
        }
    }
}
