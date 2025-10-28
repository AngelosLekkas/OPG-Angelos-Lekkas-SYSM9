using MVVM_KlonaMIg.MVVM;
using OPGslutuppgift.Managers;
using OPGslutuppgift.MVVM;
using OPGslutuppgift.Views;
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
        public ICommand LoginCommand { get; }

        public ICommand OpenRegisterCommand { get; }

        //hämta usermanager via konstruktorn
        public MainViewModel(UserManager userManager)
        {
            UserManager = userManager;

            LoginCommand = new RelayCommand(execute => Login());
            OpenRegisterCommand = new RelayCommand(execute => OpenRegisterWindow());

        }
        //MainViewModel props för {Binding}
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


        //metoder
        private void Login() //Login metod som körs när login knappen klickas
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Fyll i både användarnamn och lösenord.");
                return;
            }

            bool success = UserManager.ValidateLogin(Username, Password); 

            if (success) //om true
            {
                MessageBox.Show($"Välkommen {Username}!"); //welcome message
                RecipeListWindow recipeListWindow = new RecipeListWindow(); //skapa recipelistWindow
                recipeListWindow.Show(); //öppna recipelistWindow

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        window.Close(); //stäng mainWindow
                        break;
                    }
                }

            }
            else //annars (false)
            {
                MessageBox.Show("Fel användarnamn eller lösenord.");
            }
        }

        private void OpenRegisterWindow() //metod som ska bindas till RegisterCommand för att öppna RegisterWindow
        {
            //öppna RegisterWindow
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            //stäng MainWindow
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Close();
                    break;
                }
            }
        }

    }
}
