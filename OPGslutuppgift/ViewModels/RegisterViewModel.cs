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

        private string? _confirmPassword;
        public string? ConfirmPassword //prop för att bekräfta password vid reg (VG)
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(); }
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
        private void Register() //metod som körs när register knappen klickas (kontroll av pw krav)
        {
            if (string.IsNullOrWhiteSpace(Username) || 
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword) || 
                string.IsNullOrWhiteSpace(Country))
            {
                MessageBox.Show("Du måste fylla i alla fält!");
                return;
            }

            if (UserManager.FindUser(Username)) //kör finduser metoden från usermanager
            {
                MessageBox.Show("Username already in use.");
                return;
            }

            if (Password != ConfirmPassword) //kollar så password == confirmpw (VG)
            {
                MessageBox.Show("Lösenorden matchar inte!");
                return;
            }


            if (Password.Length < 8) //kontroll av password length (VG)
            {
                MessageBox.Show("Lösenordet måste vara minst 8 tecken långt.");
                return;
            }

            bool hasNbr = false; //variabel för kontroll av siffra i password (VG)

            foreach(char c in Password) //går igenom varje char i password
            {
                if (char.IsDigit(c)) //om char = siffra
                {
                    hasNbr = true; //true
                    break;
                }
            }
            if (!hasNbr) //om password inte har siffra
            {
                MessageBox.Show("Lösenordet måste innehålla minst en siffra!");
                return;
            }

            bool hasSpecialChar = false; //variabel för kontroll av specialtecken i pw (VG)

            foreach (char c in Password)
            {
                if (!char.IsLetterOrDigit(c)) //om char inte är bokstav eller siffra
                {
                    hasSpecialChar = true; //true (har specialtecken)
                    break;
                }
            }

            if (!hasSpecialChar)
            {
                MessageBox.Show("Lösenordet måste innehålla minst ett Specialtecken!");
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
