using OPGslutuppgift.Managers;
using OPGslutuppgift.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OPGslutuppgift.Views
{
    /// <summary>
    /// Interaction logic for RecipeListWindow.xaml
    /// </summary>
    public partial class RecipeListWindow : Window
    {
        public RecipeListWindow()
        {
            InitializeComponent();
            //hämta usermanager 
            UserManager userManager = (UserManager)Application.Current.Resources["UserManager"];
            //hämta recipemanager
            RecipeManager recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];

            //sätt datacontext
            DataContext = new RecipeListViewModel(userManager, recipeManager);
        }
    }
}
