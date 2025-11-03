using MVVM_KlonaMIg.MVVM;
using OPGslutuppgift.Managers;
using OPGslutuppgift.Models;
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
    public class ForgotPasswordViewModel : ViewModelBase
    {
        public UserManager UserManager { get; }

        //props för {Binding}
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _securityQuestion;
        public string SecurityQuestion
        {
            get { return _securityQuestion; }
            set { _securityQuestion = value; OnPropertyChanged(); }
        }

        private string _securityAnswer;
        public string SecurityAnswer
        {
            get { return _securityAnswer; }
            set { _securityAnswer = value; OnPropertyChanged(); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged(); }
        }

        private User _foundUser; //hjälpvariabel för att spara hittad user från FindUserByUsername metoden

        //commands
        public ICommand FindUserCommand { get; }
        public ICommand ResetPasswordCommand { get; }

        //konstruktor
        public ForgotPasswordViewModel(UserManager userManager)
        {
            UserManager = userManager;

            FindUserCommand = new RelayCommand(execute => FindUser());
            ResetPasswordCommand = new RelayCommand(execute => ResetPassword());
        }

        //metoder
        private void FindUser() //metod för att hitta user med username som anges
        {
            _foundUser = UserManager.FindUserByUsername(Username);

            if(_foundUser == null) //om user ej finns med det username
            {
                MessageBox.Show("User finns ej"); //felmedd
                return;
            }

            SecurityQuestion = _foundUser.SecurityQuestion; //annars securityquestion från user till prop
        }

        private void ResetPassword()
        {
            if (SecurityAnswer != _foundUser.SecurityAnswer) //om svar är fel
            {
                MessageBox.Show("Fel svar på säkerhetsfrågan"); //felmedd
                return;
            }

            if (string.IsNullOrWhiteSpace(NewPassword)) //måste fylla i nytt pw
            {
                MessageBox.Show("Fyll i nytt lösenord.");
                return;
            }

            _foundUser.Password = NewPassword; //sätt pw till newPw
            MessageBox.Show("Lösenordet har uppdaterats!");

            CloseWindow(); //stäng fönster
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ForgotPasswordWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
