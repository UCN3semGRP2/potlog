using Desktop.ServiceReference;
using Microsoft.VisualBasic;
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
        ServiceReference.IService service = new ServiceReference.ServiceClient();

        private User usr;

        public MainPage()
        {
            InitializeComponent();
            usr = service.UpdateUserInfo(((MainWindow)Application.Current.MainWindow).loggedIn);
            lblCurrentUserName.Content = usr.Firstname + " " + usr.Lastname;
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserDetail());
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

        private void btnEnterInviteString_Click(object sender, RoutedEventArgs e)
        {
            var inviteString = Interaction.InputBox("Indtast venligst den ivitationskode du har modtaget");
            Event evnt = service.AcceptInviteString(usr, inviteString);
            if (evnt == null)
            {
                MessageBox.Show("Den indtastede invitationskode er ikke korrekt");
                return;
            }
            new EventWindow(evnt, usr).Show();
        }
    }
}
