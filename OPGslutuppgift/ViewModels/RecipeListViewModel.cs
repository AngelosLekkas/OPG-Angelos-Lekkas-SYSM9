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
        public RecipeManager RecipeManager { get; }

        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes { get; set; }


        private Recipe? _selectedRecipe;
        public Recipe? SelectedRecipe 
        {
            get { return _selectedRecipe; }
            set { _selectedRecipe = value; OnPropertyChanged(); }
        }
        public string ShowCurrentUsername
        {
            get { return UserManager.CurrentUser.Username; }
        }


        private string _searchText; //prop för filter funktion (VG)
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterRecipes(); //anropar filter metod när searchtext ändras
            }
        }

        private string _selectedCategory; //prop för filter funktion (VG)
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterRecipes(); //anropar filter metod när category ändras
            }
        }

        private DateTime? _selectedDate; //prop för filter funktion (VG)
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
                FilterRecipes(); //anropar filter metod när date ändras
            }
        }

        private string _sortOrder; //prop för filter funktion (VG)
        public string SortOrder
        {
            get { return _sortOrder; }
            set
            {
                _sortOrder = value;
                OnPropertyChanged();
                FilterRecipes(); //anropar filter metod när sortorder ändras
            }
        }

        public List<string> Categories { get; } = new List<string> //lista med kategorier för filter (VG)
        {
         "Alla",
         "Middag",
         "Lunch",
         "Frukost",
         "Dessert"
        };


        public List<string> SortOrders { get; } = new List<string> //lista med alt för sortering (VG)
        {
            "Ingen sortering",
            "Senaste",
            "Äldsta"
        };


        //commands
        public ICommand AddRecipeCommand { get; }
        public ICommand RemoveRecipeCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand UserCommand { get; }
        public ICommand InfoCommand { get; }

        //konstruktor
        public RecipeListViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;

            if (UserManager.CurrentUser is AdminUser) //om currentuser är adminuser
            {
                Recipes = RecipeManager.GetAllRecipes(); //hämta alla recept
            }
            else
            {
                Recipes = RecipeManager.GetByUser(UserManager.CurrentUser); //annars bara currentusers
            }


            //bindar commands till metoder för buttons
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
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(); 
            addRecipeWindow.ShowDialog(); //öppnar addRecipeWindow

            RefreshRecipes(); //visar recept i RecipeList efter add.
        }

        private void RefreshRecipes() //metod för att uppdatera recipe list (efter add)
        {
            if (UserManager.CurrentUser is AdminUser) //om currentuser är adminuser
            {
                Recipes = RecipeManager.Recipes; //hämta alla recept
            }
            else
            {
                var userRecipes = RecipeManager.Recipes
                    .Where(recipe => recipe.Author == UserManager.CurrentUser); //annars bara currentusers
                Recipes = new ObservableCollection<Recipe>(userRecipes);
            }

            OnPropertyChanged(nameof(Recipes));
        }

        private void RemoveRecipe() //metod som körs när man klickar på remove button.
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Välj ett recept först!");
                return;
            }

            MessageBox.Show($"{SelectedRecipe.Title} receptet har tagits bort."); //skriver ut vilket recept som tas bort
            RecipeManager.RemoveRecipe(SelectedRecipe); //tar bort recept (anropar recipemanager remove metod)

            RefreshRecipes(); //uppdaterar recipe list efter remove
        }

        private void ShowDetails() //metod för att visa recipe details (när man klickar på details knappen)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Välj ett recept först!");
                return;
            }

            RecipeDetailWindow detailsWindow = new RecipeDetailWindow(SelectedRecipe, RecipeManager);
            detailsWindow.ShowDialog();
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

        private void FilterRecipes() //filter metod för att apply filter (VG)
        {
            ObservableCollection<Recipe> filteredRecipes;
            if(UserManager.CurrentUser is AdminUser) //om currentuser är admin
            {
                filteredRecipes = RecipeManager.GetAllRecipes(); //hämta alla recept
            }

            else
            {
                filteredRecipes = RecipeManager.GetByUser(UserManager.CurrentUser); //annars bara currentusers
            }

            List<Recipe> filterResult = new List<Recipe>(); //tom lista för filtrerade recept

            foreach(Recipe recipe in filteredRecipes)
            {
                bool matchingRecipe = true; //bool för kontroll av matchande recipe

                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    if(!recipe.Title.Contains(SearchText) && !recipe.Category.Contains(SearchText)) //om searchtext inte finns (title/category)
                    {
                        matchingRecipe = false;
                    }
                }

                if (matchingRecipe == true && !string.IsNullOrWhiteSpace(SelectedCategory)) //om category är vald
                {
                    if (SelectedCategory != "Alla") //om vald category != alla
                    {
                        if(recipe.Category != SelectedCategory) //om recipe category inte är samma som vald category
                        {
                            matchingRecipe = false;
                        }
                    }
                }

                if(matchingRecipe == true && SelectedDate != null) //om datum valts
                {
                    if(recipe.Date.Date != SelectedDate.Value.Date) //om recipe date inte matchar vald date
                    {
                        matchingRecipe = false;
                    }
                }

                if(matchingRecipe == true) //om ALLA filters matchar recipe
                {
                    filterResult.Add(recipe); //lägg till recipe i filterlistan
                }
            }

            if(SortOrder == "Senaste")
            {
                filterResult.Sort((a, b) => b.Date.CompareTo(a.Date)); //sort från senaste till äldsta
            }

            else if (SortOrder == "Äldsta")
            {
                filterResult.Sort((a, b) => a.Date.CompareTo(b.Date)); //sort från äldsta till senaste
            }

            Recipes = new ObservableCollection<Recipe>(filterResult); //lägger in recipes i filterlistan

            OnPropertyChanged(nameof(Recipes)); //uppdaterar recipelist i view

        }
    }
}
