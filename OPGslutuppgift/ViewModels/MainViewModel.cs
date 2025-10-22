using OPGslutuppgift.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPGslutuppgift.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
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
