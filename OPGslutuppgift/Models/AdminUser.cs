using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPGslutuppgift.Models
{
    public class AdminUser : User
    {
        public AdminUser (string Username, string Password, string Country) : base(Username, Password, Country) { } //konstruktor från User

        public void RemoveAnyRecipe(Recipe recipe, RecipeManager manager) //metod för adminUser att ta bort recept.
        {
            manager.
        }
    }
}
