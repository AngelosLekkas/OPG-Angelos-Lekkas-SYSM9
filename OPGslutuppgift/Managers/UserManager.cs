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


        //props
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

        //konstruktor
        public UserManager()
        {
            _users = new ObservableCollection<User>(); //initierar user list
            CreateDefaultUsers();
        }


        //metoder
        private void CreateDefaultUsers() //skapar default users med username & password.
        {
            var admin = new AdminUser //admin
            {
                Username = "admin",
                Password = "password",
                Country = "Sweden",
                SecurityQuestion = "Vad heter din lärare i OPG?",
                SecurityAnswer = "hassan"
            };

            Users.Add(admin);

            var user = new User //user
            {
                Username = "user",
                Password = "password",
                Country = "Sweden",
                SecurityQuestion = "Vad heter din lärare i OPG?",
                SecurityAnswer = "hassan"
            };
            Users.Add(user);


            //lägger till 2 default recept för user (admin kommer också kunna se, då admin ser alla recept)?
            Application.Current.Dispatcher.Invoke(() => 
            {
                var recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"]; //hämtar recipemanager

                recipeManager.AddRecipe(new Recipe 
                {
                    Title = "Pasta Bolognese",
                    Category = "Middag",
                    Ingredients = "Pasta, köttfärs, tomat",
                    Instructions = "Koka pasta, koka sås, blanda.",
                    Date = DateTime.Now,
                    Author = user
                });

                recipeManager.AddRecipe(new Recipe
                {
                    Title = "Chokladkaka",
                    Category = "Dessert",
                    Ingredients = "Smör, socker, kakao, ägg, mjöl",
                    Instructions = "Blanda, grädda 20 min.",
                    Date = DateTime.Now,
                    Author = user
                });
            });
        }

        public bool ValidateLogin(string username, string password)
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
            var newUser = new User //skapar new user i users list
            {
                Username = username,
                Password = password,  //sätter props till inskickade parametrar
                Country = country,
                SecurityQuestion = "Vad heter din lärare i OPG?", //lägger till för att nya users också ska ha säkerhetsfråga & svar
                SecurityAnswer = "hassan"
            };
            Users.Add(newUser);

            RecipeManager recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"]; //hämtar recipemanager

            recipeManager.DefaultRecipesNewUser(newUser); //anropar metod för att lägga till default recept för newuser
        }

        public bool FindUser(string username) //metod för att se om username redan existerar i users list.
        {
            foreach (User user in Users) //går igenom listan
            {
                if (user.Username.Equals(username)) //om username redan finns
                {
                    //MessageBox.Show("Username already exists."); //felmeddelande
                    return true;
                }
            }
            return false;
        }

        public User? FindUserByUsername(string username) //metod för hitta user (VG) (anropas i ForgotPasswordViewModel)
        {
            foreach(User user in Users) //sök igenom user list
            {
                if(user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) //om user med samma username finns
                {
                    return user; //returnera user
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
