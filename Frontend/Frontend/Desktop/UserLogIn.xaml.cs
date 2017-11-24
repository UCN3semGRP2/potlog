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
    /// Interaction logic for UserLogIn.xaml
    /// </summary>
    public partial class UserLogIn : Page
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        public UserLogIn()
        {
            InitializeComponent();
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = this.Txt_Email.Text;
            string pw = this.Pw_Password.Password;

            var user = service.LogIn(email, pw);
            if (user == null)
            {
                MessageBox.Show("Email or Password is incorrect");
                return;
            }
            ((MainWindow)Application.Current.MainWindow).loggedIn = user;
            this.NavigationService.Navigate(new MainPage());
        }

        private void Btn_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateUser());
        }
    }
}
