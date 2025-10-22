using OPGslutuppgift.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OPGslutuppgift.Managers
{
    public class UserManager : INotifyPropertyChanged
    {
        //hantera nuvarande user (CurrentUser)
        //hantera login, register, findUser, changePassword & CurrentUser (metoder)

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
        }


        private User? _currentUser; //hjälpvariabel current user
        public User? CurrentUser 
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

        private void CreateDefaultUsers() //skapar default users
        {
            Users.Add(new User
            {
                Username = "Admin",
                Password = "1234"
            });

            Users.Add(new User
            {
                Username = "User",
                Password = "1234"
            });
        }

        public bool Login(string username, string password)
        {
            //jämföra username & password med users från listan,
            //om username & password finns ->
            //sätt currentUser till user.

            foreach (User u in Users)
            {
                if (u.Username == username && u.Password == password)
                {
                    CurrentUser = u;
                    return true;
                }
            }
            return false;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
