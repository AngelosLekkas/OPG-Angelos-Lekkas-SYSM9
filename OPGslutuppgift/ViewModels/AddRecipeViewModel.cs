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
    public class AddRecipeViewModel : ViewModelBase
    {
        public UserManager UserManager { get; }
        public RecipeManager RecipeManager { get; }

        //props för {Binding}
        public string Title { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }

        //commands
        public ICommand RecipeSaveCommand { get; }
        public ICommand RecipeCancelCommand { get; }


        //konstruktor
        public AddRecipeViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;

            RecipeSaveCommand = new RelayCommand(execute => SaveRecipe());
            RecipeCancelCommand = new RelayCommand(execute => CloseWindow());
        }


        //metoder
        private void SaveRecipe() //metod för save knapp
        {
            if (string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Ingredients) ||
                string.IsNullOrWhiteSpace(Instructions))
            {
                MessageBox.Show("Alla fält måste fyllas i!"); //om fält = tomt, felmedd
                return;
            }

            var newRecipe = new Recipe //variabel för nytt recept
            {
                Title = Title,
                Category = Category,
                Ingredients = Ingredients,
                Instructions = Instructions,
                Date = DateTime.Now,
                Author = UserManager.CurrentUser
            };

            RecipeManager.AddRecipe(newRecipe); //lägger till recept i listan (via RecipeManager)
            MessageBox.Show($"Receptet {Title} har lagts till!");


            foreach(Window window in Application.Current.Windows) //hitta öppet recipelistWindow
            {
                if(window is RecipeListWindow recipeListWindow) 
                {
                    //hämta viewmodel från RecipeListWindow
                    var viewmodel = recipeListWindow.DataContext as RecipeListViewModel;
                    if(viewmodel != null)
                    {
                        if(UserManager.CurrentUser is AdminUser) //om admin, hämta alla recipes
                        {
                            viewmodel.Recipes = RecipeManager.GetAllRecipes();
                        }
                        else
                        {
                            viewmodel.Recipes = RecipeManager.GetByUser(UserManager.CurrentUser);//annars bara currentusers
                        }

                        viewmodel.OnPropertyChanged(nameof(viewmodel.Recipes)); //uppdatera UI
                    }
                    break;
                }
            }

            CloseWindow();
        }

        private void CloseWindow() //metod för stänga AddRecipeWindow
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Views.AddRecipeWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
