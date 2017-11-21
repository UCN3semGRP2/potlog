using Desktop.ServiceReference;
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

namespace Desktop
{
    /// <summary>
    /// Interaction logic for ShowCaseWindow.xaml
    /// </summary>
    public partial class ShowCaseWindow : Window
    {
        public ShowCaseWindow()
        {
            InitializeComponent();
            Loaded += Load_Window;
        }

        private void Load_Window(object sender, RoutedEventArgs e)
        {
            CreateEventFrame.NavigationService.Navigate(new CreateEvent());
            CreateUserFrame.NavigationService.Navigate(new CreateUser());

            ServiceReference.IService service = new ServiceReference.ServiceClient();
            var ev = service.FindEventById(1);
            var fakeUser = new User();
            var edPage = new EventDetails(ev, fakeUser);
            EventDetailsFrame.NavigationService.Navigate(edPage);
        }
    }
}
