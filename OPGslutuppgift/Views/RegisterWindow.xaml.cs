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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            //hämta usermanager 
            UserManager userManager = (UserManager)Application.Current.Resources["UserManager"];

            //skapa instans av viewmodel
            RegisterViewModel viewModel = new RegisterViewModel(userManager);

            //sätta datacontext till viewmodellen
            DataContext = viewModel;
        }
    }
}
