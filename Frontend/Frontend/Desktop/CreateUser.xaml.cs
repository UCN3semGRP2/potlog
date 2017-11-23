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
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Page
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();
        public CreateUser()
        {
            InitializeComponent();
        }

        private void btn_CreatUser_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Password.Text.Equals(tb_RepeatPassword.Text) 
                && tb_Email.Text.Length != 0 
                && tb_Firstname.Text.Length != 0 
                && tb_Lastname.Text.Length != 0
                && tb_Password.Text.Length >= 6)
            {
                service.CreateUser(tb_Firstname.Text, tb_Lastname.Text, tb_Email.Text, tb_Password.Text);
                MessageBox.Show("Bruger er oprettet.");
            }
            else
            {
                MessageBox.Show("Vær sød at indtaste alle de nødvendige oplysninger.");
            }

            this.NavigationService.Navigate(new UserLogIn());
        }

        private void tb_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Password.Text.Length < 6)
            {
                tb_Password.BorderBrush = Brushes.Red;
            }
            else
            {
                tb_Password.BorderBrush = Brushes.Black;
            }
        }

        private void tb_RepeatPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Password.Text.Equals(tb_RepeatPassword.Text))
            {
                tb_RepeatPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                tb_RepeatPassword.BorderBrush = Brushes.Black;
            }
        }

        private void tb_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!tb_Email.Text.Contains("@"))
            {
                tb_Email.BorderBrush = Brushes.Red;
            }
            else
            {
                tb_Email.BorderBrush = Brushes.Black;
            }
        }

        private void tb_Firstname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Firstname.Text.Length < 1)
            {
                tb_Firstname.BorderBrush = Brushes.Red;
            }
            else
            {
                tb_Firstname.BorderBrush = Brushes.Black;
            }
        }

        private void tb_Lastname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Lastname.Text.Length < 1)
            {
                tb_Lastname.BorderBrush = Brushes.Red;
            }
            else
            {
                tb_Lastname.BorderBrush = Brushes.Black;
            }
        }
    }
}
