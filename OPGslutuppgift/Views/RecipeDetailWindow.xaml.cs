using OPGslutuppgift.Managers;
using OPGslutuppgift.Models;
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
    /// Interaction logic for RecipeDetalWindow.xaml
    /// </summary>
    public partial class RecipeDetailWindow : Window
    {
        public RecipeDetailWindow(Recipe selectedRecipe, RecipeManager recipeManager)
        {
            InitializeComponent();

            // skapa instans av viewmodel
            RecipeDetailViewModel viewModel = new RecipeDetailViewModel(selectedRecipe, recipeManager);

            // sätt datacontext
            DataContext = viewModel;
        }
    }
}
