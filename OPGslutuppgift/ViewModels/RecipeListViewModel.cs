using MVVM_KlonaMIg.MVVM;
using OPGslutuppgift.Managers;
using OPGslutuppgift.Models;
using OPGslutuppgift.MVVM;
using OPGslutuppgift.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPGslutuppgift.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        //props
        public UserManager UserManager { get; }
        public ObservableCollection<Recipe> Recipes { get; set; }


        private Recipe? _selectedRecipe;
        public Recipe? SelectedRecipe 
        {
            get { return _selectedRecipe; }
            set { _selectedRecipe = value; OnPropertyChanged(); }
        }
        public string CurrentUsername
        {
            get { return UserManager.CurrentUser.Username; }
        }
        //commands
        public ICommand AddRecipeCommand { get; }
        public ICommand RemoveRecipeCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand UserCommand { get; }
        public ICommand InfoCommand { get; }

        //konstruktor
        public RecipeListViewModel(UserManager userManager)
        {
            UserManager = userManager;

            
            Recipes = new ObservableCollection<Recipe> //skapar tre default recept
            {
                new Recipe { Title="Pasta", Category="Italienskt", Author=userManager.CurrentUser.Username },
                new Recipe { Title="Tacos", Category="Mexikanskt", Author=userManager.CurrentUser.Username },
                new Recipe { Title="Köttbullar", Category="Svenskt", Author=userManager.CurrentUser.Username }
            };

            //bindar commands till metoder
            AddRecipeCommand = new RelayCommand(execute => AddRecipe());
            RemoveRecipeCommand = new RelayCommand(execute => RemoveRecipe());
            DetailsCommand = new RelayCommand(execute => ShowDetails());
            SignOutCommand = new RelayCommand(execute => SignOut());
            UserCommand = new RelayCommand(execute => OpenUserDetails());
            InfoCommand = new RelayCommand(execute => ShowInfo());
        }

        //metoder
        private void AddRecipe() //metod som körs när man klickar på add knappen.
        {
            MessageBox.Show("AddRecipe window ska öppnas");
        }

        private void RemoveRecipe() //metod som körs när man klickar på remove button.
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Välj ett recept först!");
                return;
            }

            MessageBox.Show($"{SelectedRecipe.Title} receptet har tagits bort."); //skriver ut vilket recept som tas bort
            Recipes.Remove(SelectedRecipe); //tar bort recept
        }

        private void ShowDetails() //metod för att visa recipe details (när man klickar på details knappen)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Välj ett recept först!");
                return;
            }

            MessageBox.Show($"Detaljer för {SelectedRecipe.Title}");
        }

        private void SignOut() //metod för att logga ut user/returnera till mainWindow (Sign Out knappen)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); //går tillbaka till mainwindow

            foreach (Window window in Application.Current.Windows)
            {
                if (window is RecipeListWindow)
                {
                    window.Close(); //stänger recipelistWindow
                    break;
                }
            }
        }
        private void OpenUserDetails()//metod för att öppna userDetailsWindow (när man klickar på user button)
        {
            UserDetailsWindow userDetails = new UserDetailsWindow();
            userDetails.ShowDialog();
        }
        private void ShowInfo()//metod för att visa info om programmet
        {
            MessageBox.Show(
                "CookMaster är en digital receptbok.\n\nHär kan du skapa, visa och hantera dina favoritrecept! \n\nUtvecklad av Angelos Lekkas, 2025. ",
                "Om CookMaster");
        }
    }
}
