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

        public ObservableCollection<Recipe> GetAllRecipes() //metod för att hämta alla recept
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

        //public void DefaultRecipesNewUser(User user) //metod för att lägga till default recept för alla newUsers (inte bara admin, user)
        //{
        //    AddRecipe(new Recipe
        //    {
        //        Title = "Pasta Bolognese",
        //        Category = "Middag",
        //        Ingredients = "Pasta, köttfärs, tomatsås",
        //        Instructions = "Koka pastan, laga såsen, blanda allt.",
        //        Date = DateTime.Now,
        //        Author = user
        //    });

        //    AddRecipe(new Recipe
        //    {
        //        Title = "Chokladkaka",
        //        Category = "Dessert",
        //        Ingredients = "Smör, socker, kakao, ägg, mjöl",
        //        Instructions = "Blanda och grädda i 20 minuter.",
        //        Date = DateTime.Now,
        //        Author = user
        //    });
        //}



        //public void UpdateRecipe(Recipe recipe) //metod för att uppdatera recept (edit)
        //{
        //    OnPropertyChanged(nameof(Recipes));
        //}


        //public ObservableCollection<Recipe> Filter(string criteria) //metod för att filtrera recept baserat på titel eller kategori
        //{
        //    return null;
        //}

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
