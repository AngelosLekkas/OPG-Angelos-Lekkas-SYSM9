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
    public class RecipeManager : INotifyPropertyChanged
    {
        //props
        private ObservableCollection<Recipe> _recipes;

        public ObservableCollection<Recipe> Recipes  //skapar recipe list
        {
            get { return _recipes; }
            set { _recipes = value; OnPropertyChanged(); }
        }

        // konstruktor
        public RecipeManager() 
        {
            _recipes = new ObservableCollection<Recipe>();
        }

        //metoder
        public void AddRecipe(Recipe recipe) //add metod för att lägga till recept (anropas i AddRecipeViewModel)
        {
            _recipes.Add(recipe);
            OnPropertyChanged(nameof(Recipes));
        }

        public void RemoveRecipe(Recipe recipe) //remove metod för att ta bort recept (anropas i recipelistViewmodel)
        {
            _recipes.Remove(recipe);
            OnPropertyChanged(nameof(Recipes));
        }

        public ObservableCollection<Recipe> GetAllRecipes() //metod för att hämta alla recept (adminUser)
        {
            return Recipes;
        }

        public ObservableCollection<Recipe> GetByUser(User user) //metod för att hämta recept av en specifik user
        {
            var list = new ObservableCollection<Recipe>();
            foreach (var recipe in Recipes)
            {
                if (recipe.Author == user)
                    list.Add(recipe);
            }
            return list;
        }

        public void UpdateRecipe(Recipe recipe) //metod för att uppdatera recept (edit)
        {
        }


        public ObservableCollection<Recipe> Filter(string criteria) //metod för att filtrera recept baserat på titel eller kategori
        {
            return null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
