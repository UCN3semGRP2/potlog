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
        public CreateUser()
        {
            InitializeComponent();
        }

        private void btn_CreatUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tb_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Password.Text.Length < 6)
            {
                tb_Password.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                tb_Password.BorderBrush = System.Windows.Media.Brushes.Black;
            }
        }

        private void tb_RepeatPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Password.Text.Equals(tb_RepeatPassword.Text))
            {
                tb_RepeatPassword.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                tb_RepeatPassword.BorderBrush = System.Windows.Media.Brushes.Black;
            }
        }
    }
}
