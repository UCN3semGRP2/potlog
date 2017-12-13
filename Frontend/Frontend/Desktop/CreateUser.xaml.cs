using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

    public partial class CreateUser : Page
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();
        public CreateUser()
        {
            InitializeComponent();
            lblErrorMsg.Visibility = Visibility.Hidden;
        }

        private void btn_CreatUser_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateHelper.validateRepeatPassword(tb_Password.Password, tb_RepeatPassword.Password)
                && ValidateHelper.validateEmail(tb_Email.Text)
                && tb_Firstname.Text.Length != 0
                && tb_Lastname.Text.Length != 0
                && ValidateHelper.validatePassword(tb_Password.Password))
            {
                try
                {
                    lblErrorMsg.Visibility = Visibility.Hidden;
                    service.CreateUser(tb_Firstname.Text, tb_Lastname.Text, tb_Email.Text, tb_Password.Password);
                }
                catch (FaultException fault)
                {
                    lblErrorMsg.Visibility = Visibility.Visible;
                    lblErrorMsg.Content = fault.Message;
                    lblErrorMsg.Foreground = Brushes.Red;
                    return;
                }
                MessageBox.Show("Bruger er oprettet.");
            }
            else
            {
                MessageBox.Show("Vær sød at indtaste alle de nødvendige oplysninger.");
                return;
            }

            this.NavigationService.Navigate(new UserLogIn());
        }


        private void tb_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!tb_Email.Text.Contains("@"))
            {
                tb_Email.BorderBrush = Brushes.Red;
                tb_Email.ToolTip = "Den indtasted email er ikke valid";
            }
            else
            {
                tb_Email.BorderBrush = Brushes.Black;
                tb_Email.ToolTip = null;
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserLogIn());
        }

        private void tb_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!ValidateHelper.validatePassword(tb_Password.Password))
            {
                tb_Password.BorderBrush = Brushes.Red;
                tb_Password.ToolTip = "Kodeordet skal mindst være 6 tegn langt";
            }
            else
            {
                tb_Password.BorderBrush = Brushes.Black;
                tb_Password.ToolTip = null;
            }
            tb_RepeatPassword_PasswordChanged(sender, e);
        }

        private void tb_RepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!tb_Password.Password.Equals(tb_RepeatPassword.Password))
            {
                tb_RepeatPassword.BorderBrush = Brushes.Red;
                tb_RepeatPassword.ToolTip = "Kodeordene er ikke det samme";
            }
            else
            {
                tb_RepeatPassword.BorderBrush = Brushes.Black;
                tb_RepeatPassword.ToolTip = null;
            }
        }
    }
}
