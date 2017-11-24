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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            lblCurrentUserName.Content = ((MainWindow)Application.Current.MainWindow).loggedIn.Firstname + ((MainWindow)Application.Current.MainWindow).loggedIn.Lastname;
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            Page TODO = new Page();
            this.NavigationService.Navigate(TODO);
        }

        private void btnCreateNewEvent_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateEvent());
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).loggedIn = null;
            this.NavigationService.Navigate(new UserLogIn());
        }
    }
}
