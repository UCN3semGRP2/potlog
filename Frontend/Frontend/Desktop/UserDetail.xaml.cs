﻿using System;
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
    /// Interaction logic for UserDetail.xaml
    /// </summary>
    public partial class UserDetail : Page
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();
        public UserDetail()
        {
            InitializeComponent();
            tb_Email.Text = ((MainWindow)Application.Current.MainWindow).loggedIn.Email;
            tb_Firstname.Text = ((MainWindow)Application.Current.MainWindow).loggedIn.Firstname;
            tb_Lastname.Text = ((MainWindow)Application.Current.MainWindow).loggedIn.Lastname;
        }

        private void btn_CreatUser_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Password.Text.Equals(tb_RepeatPassword.Text)
                && tb_Email.Text.Length != 0
                && tb_Firstname.Text.Length != 0
                && tb_Lastname.Text.Length != 0
                && tb_Password.Text.Length >= 6)
            {
                //TODO
                MessageBox.Show("Brugeroplysninger er blevet opdateret");
            }
            else
            {
                MessageBox.Show("Vær sød at indtaste alle de nødvendige oplysninger.");
            }

            this.NavigationService.Navigate(new MainPage());
        }

        private void tb_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Password.Text.Length < 6)
            {
                tb_Password.BorderBrush = Brushes.Red;
                tb_Password.ToolTip = "Kodeordet skal mindst være 6 tegn langt";
            }
            else
            {
                tb_Password.BorderBrush = Brushes.Black;
                tb_Password.ToolTip = null;
            }
        }

        private void tb_RepeatPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!tb_Password.Text.Equals(tb_RepeatPassword.Text))
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
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
