using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPGslutuppgift.Models
{
    public class AdminUser : User //ärver från User klassen
    {
        public AdminUser (string Username, string Password, string Country) : base(Username, Password, Country) { } //konstruktor från User

        public void RemoveAnyRecipe() //metod för adminUser att ta bort recept.
        {
            
        }

        public void ViewAllRecipes() //metod för adminUser att se alla recept.
        {

        }
    }
}
