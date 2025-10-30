using MVVM_KlonaMIg.MVVM;
using OPGslutuppgift.Managers;
using OPGslutuppgift.Models;
using OPGslutuppgift.MVVM;
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
        private void SaveRecipe()
        {
            if (string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Ingredients) ||
                string.IsNullOrWhiteSpace(Instructions))
            {
                MessageBox.Show("Alla fält måste fyllas i!");
                return;
            }

            var newRecipe = new Recipe
            {
                Title = Title,
                Category = Category,
                Ingredients = Ingredients,
                Instructions = Instructions,
                Date = DateTime.Now,
                Author = UserManager.CurrentUser
            };

            RecipeManager.AddRecipe(newRecipe);
            MessageBox.Show($"Receptet {Title} har lagts till!");

            CloseWindow();
        }

        private void CloseWindow()
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
