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
    /// Interaction logic for UserLogIn.xaml
    /// </summary>
    public partial class UserLogIn : Page
    {

        public UserLogIn()
        {
            InitializeComponent();
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = this.Txt_Email.Text;
            string pw = this.Pw_Password.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
