using MVVM_KlonaMIg.MVVM;
using OPGslutuppgift.Managers;
using OPGslutuppgift.MVVM;
using OPGslutuppgift.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPGslutuppgift.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        //skapa usermanager prop
        public UserManager UserManager { get; }
        public ICommand RegisterCommand { get; }

        //hämta usermanager via konstruktorn
        public RegisterViewModel(UserManager userManager)
        {
            UserManager = userManager;
            RegisterCommand = new RelayCommand (execute => Register());
        }

        //RegisterViewModel props för {Binding}
        private string? _username;
        public string? Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private string? _country;
        public string? Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(); }
        }

        public List<string> Countries { get; } = new List<string> //public lista med countries (för combobox)
        {
         "Sverige",
         "Norge",
         "Danmark"
        };

        //metoder
        private void Register() //metod som körs när register knappen klickas
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Country))
            {
                MessageBox.Show("Du måste fylla i alla fält!");
                return;
            }

            if (UserManager.FindUser(Username)) //kör finduser metoden från usermanager
            {
                MessageBox.Show("Username already in use.");
                return;
            }
             

            UserManager.Register(Username, Password, Country); //kör register metoden från usermanager för att lägga till user i listan.
            MessageBox.Show("Användare skapad!");

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //stänger ner registerWindow
            CloseWindow();
        }
        private void CloseWindow() //metod som stänger registerwindow efter registrering
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is RegisterWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
