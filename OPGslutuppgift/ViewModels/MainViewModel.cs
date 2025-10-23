using MVVM_KlonaMIg.MVVM;
using OPGslutuppgift.Managers;
using OPGslutuppgift.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPGslutuppgift.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //skapa usermanager prop
        public UserManager UserManager { get; }



        //hämta usermanager via konstruktorn
        public MainViewModel(UserManager userManager)
        {
            UserManager = userManager;
            LoginCommand = new RelayCommand(execute => Login());
        }

        private string? _username;
        public string? Username 
        {  
            get { return _username; } 
            set {_username = value; OnPropertyChanged(); }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        //private string? _country;
        //public string? Country
        //{
        //    get { return _country; }
        //    set { _country = value; OnPropertyChanged(); }
        //}

        public ICommand LoginCommand { get; }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Fyll i både användarnamn och lösenord.");
                return;
            }

            bool success = UserManager.ValidateLogin(Username, Password);

            if (success)
            {
                MessageBox.Show($"Välkommen {Username}!");
                
            }
            else
            {
                MessageBox.Show("Fel användarnamn eller lösenord.");
            }
        }

    }
}
