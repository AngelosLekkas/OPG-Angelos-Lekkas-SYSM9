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
    public class RecipeDetailViewModel : ViewModelBase
    {
        //props
        public RecipeManager RecipeManager { get; }
        public Recipe SelectedRecipe { get; }


        private bool _isReadOnly = true;
        public bool IsReadOnly //bool variabel för att binda textbox isreadonly i xaml och kunna toggla med edit knapp
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; OnPropertyChanged(); }
        }


        private bool _isEditEnabled = true;
        public bool IsEditEnabled //bool variabel för att binda edit knappens isenabled i xaml
        {
            get { return _isEditEnabled; }
            set { _isEditEnabled = value; OnPropertyChanged(); }
        }

        //commands
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        //konstruktor
        public RecipeDetailViewModel(Recipe selectedRecipe, RecipeManager recipeManager)
        {
            SelectedRecipe = selectedRecipe;
            RecipeManager = recipeManager;

            EditCommand = new RelayCommand(execute => ToggleEdit());
            SaveCommand = new RelayCommand(execute => SaveChanges());
            CancelCommand = new RelayCommand(execute => CloseWindow());
        }


        //metoder
        private void ToggleEdit() //metod för edit knapp 
        {
            IsReadOnly = false; //sätter readonly binding på textboxar till false
            IsEditEnabled = false; //"disablar" edit knappen
        }

        private void SaveChanges() //´metod för save btn
        {
            if (string.IsNullOrWhiteSpace(SelectedRecipe.Title) || //om någon av fälten = tomt
                string.IsNullOrWhiteSpace(SelectedRecipe.Category) ||
                string.IsNullOrWhiteSpace(SelectedRecipe.Ingredients) ||
                string.IsNullOrWhiteSpace(SelectedRecipe.Instructions))
            {
                MessageBox.Show("Alla fält måste fyllas i!"); //felmedd
                return;
            }

            //RecipeManager.UpdateRecipe(SelectedRecipe); //anropar update metod i RecipeManager (sparar ändringar)
            MessageBox.Show($"Receptet för {SelectedRecipe.Title} har uppdaterats"); //medd om success

            IsReadOnly = true; //sätter textboxar till readonly igen
            IsEditEnabled = true; //"enablar" edit knappen igen

            CloseWindow(); //stänger fönstret
        }

        private void CloseWindow() //metod för stänga recipedetail window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is RecipeDetailWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
