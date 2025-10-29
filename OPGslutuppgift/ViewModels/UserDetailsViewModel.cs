using MVVM_KlonaMIg.MVVM;
using OPGslutuppgift.Managers;
using OPGslutuppgift.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OPGslutuppgift.ViewModels
{
    public class UserDetailsViewModel : ViewModelBase
    {
        //props
        public UserManager UserManager { get; }

        private string _newUsername;
        public string NewUsername
        {
            get { return _newUsername; }
            set { _newUsername = value; OnPropertyChanged(); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged(); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        private string _newCountry;
        public string NewCountry
        {
            get { return _newCountry; }
            set { _newCountry = value; OnPropertyChanged(); }
        }

        public string CurrentUsername //för display av current Username
        {
            get
            {
                if (UserManager.CurrentUser != null) //om ej null
                {
                    return UserManager.CurrentUser.Username; //hämta username
                }
                else
                {
                    return ""; //annars tomt
                }
            }
        }

        public string CurrentCountry //display av current country
        {
            get
            {
                if (UserManager.CurrentUser != null)
                {
                    return UserManager.CurrentUser.Country;
                }
                else
                {
                    return "";
                }
            }
        }
        //commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        //konstruktor
        public UserDetailsViewModel(UserManager userManager)
        {
            UserManager = userManager;
            
        }

        
    }
}
