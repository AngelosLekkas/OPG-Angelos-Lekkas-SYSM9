using OPGslutuppgift.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPGslutuppgift.Managers
{
    public class UserManager : INotifyPropertyChanged
    {
        //hantera nuvarande user (CurrentUser)
        //hantera login, register, findUser, changePassword & CurrentUser (metoder)

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users //skapar user list
        {
            get { return _users; }
        }


        private User? _currentUser; 
        public User? CurrentUser //sätter nuvarande user till currentuser.
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public UserManager()
        {
            _users = new ObservableCollection<User>(); //initierar user list
            CreateDefaultUsers();
        }

        private void CreateDefaultUsers() //skapar default users med username & password.
        {
            Users.Add(new AdminUser
            {
                Username = "Admin",
                Password = "1234",
             
            });

            Users.Add(new User
            {
                Username = "User",
                Password = "1234",
               
            });
        }

        public bool Login(string username, string password)
        {
            //jämför username & password input med users från listan
            //om username & password finns ->
            

            foreach (User user in Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    CurrentUser = user; //sätt currentUser till user.
                    return true;
                }
            }
            return false;
        }

        public void Register(string username, string password, string country) //register metod med parameter som krävs för new user
        {
            Users.Add(new User //skapar new user i users list
            {
                Username = username,
                Password = password,  //sätter props till inskickade parametrar
                Country = country
            });
        }

        public void FindUser(string username) //metod för att se om username redan existerar i users list.
        {
            foreach (User user in Users) //går igenom listan
            {
                if (user.Username.Equals(username)) //om username redan finns
                {
                    MessageBox.Show("Username already exists."); //felmeddelande
                    break;
                }
            }
        }
            


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
