using MVVM_KlonaMIg.MVVM;
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
    public class TwoFactorViewModel : ViewModelBase
    {
        //props för {binding}
        public string GeneratedCode { get; set; } // prop för random kod
        public string DisplayedCode { get; set; } // prop för displaya koden
        public string EnteredCode { get; set; }   // users inputkod


        //commands
        public ICommand VerifyCommand { get; }

        //konstruktor
        public TwoFactorViewModel(string code)
        {
            GeneratedCode = code;
            DisplayedCode = $"Kod: {code}"; 
            VerifyCommand = new RelayCommand(execute => VerifyCode());
        }

        //metoder
        private void VerifyCode() //metod för att verifiera koden
        {
            if (EnteredCode == GeneratedCode)
            {
                MessageBox.Show("Verifiering godkänd!");

                foreach (Window window in Application.Current.Windows) //stäng 2faWindow
                {
                    if (window is Views.TwoFactorWindow twoFactorWindow)
                    {
                        twoFactorWindow.DialogResult = true; //returnerar true till Login() i mainViewmodel
                        twoFactorWindow.Close();
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Fel kod, försök igen."); //annars felmedd
            }
        }
    }
}
