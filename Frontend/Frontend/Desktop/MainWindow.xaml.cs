using Desktop.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class MainWindow : Window
    {
        internal User loggedIn = null;
        ServiceReference.IService service = new ServiceReference.ServiceClient();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Load_Window;

        }
        private void Load_Window(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new UserLogIn());
        }


    }
}
