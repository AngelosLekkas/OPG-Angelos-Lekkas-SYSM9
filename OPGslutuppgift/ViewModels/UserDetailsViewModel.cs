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

        public List<string> Countries { get; } = new List<string> //public lista med countries (för combobox)
        {
         "Sverige",
         "Norge",
         "Danmark"
        };


        //commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        //konstruktor
        public UserDetailsViewModel(UserManager userManager)
        {
            UserManager = userManager;


            SaveCommand = new RelayCommand(execute => SaveChanges());
            CancelCommand = new RelayCommand(execute => Cancel());
        }

        //metoder
        private void SaveChanges() //metod för save knapp (kontroll av NewUsername, NewPassword, NewCountry)
        {
            if (!string.IsNullOrWhiteSpace(NewUsername)) //om newusername inte är tom
            {
                if(NewUsername.Length < 3) //om den är mindre än 3 chars
                {
                    MessageBox.Show("Username måste vara minst 3 tecken långt!"); //felmeddelande
                    return;
                }

                if(UserManager.FindUser(NewUsername) && NewUsername != UserManager.CurrentUser.Username)
                //använder FindUser metod från UserManager (kolla om username redan finns)
                //om newusername readan finns och inte är samma som current username
                {
                    MessageBox.Show("Username already exists."); //felmedd
                    return;
                }
                UserManager.CurrentUser.Username = NewUsername; //annars sätt currentUsername till newUsername
                OnPropertyChanged(nameof(CurrentUsername)); 
            }

            if (!string.IsNullOrWhiteSpace(NewPassword)) //om newpassword inte är tom
            {
                if (NewPassword.Length < 5) //om den är mindre än 5 chars
                {
                    MessageBox.Show("Password måste vara minst 5 tecken långt!"); //felmeddelande
                    return;
                }
                if (NewPassword != ConfirmPassword) //om newpassword och confirmpassword inputs inte är samma
                {
                    MessageBox.Show("Lösenorden matchar inte!"); //felmedd
                    return;
                }
                UserManager.CurrentUser.Password = NewPassword; //annars sätt current password till newpassword
            }

            if (!string.IsNullOrWhiteSpace(NewCountry)) //om newcountry inte är tom
            {
                UserManager.CurrentUser.Country = NewCountry; //sätt current country till newcountry
            }

            MessageBox.Show("Användaruppgifter uppdaterade!"); //meddelande om success av save

            CloseWindow(); //stäng fönstret (behöver ej öppna RecipeListWindow eftersom den ej stängdes)

        }

        private void Cancel() //metod för cancel knapp
        {
            CloseWindow(); //(tillbaka till RecipeListWindow)
        }

        private void CloseWindow() //metod för att stänga userdetailsWindow
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is UserDetailsWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
