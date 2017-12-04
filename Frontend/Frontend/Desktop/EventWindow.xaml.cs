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
using Desktop.ServiceReference;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        private Event evnt;
        private User user;

        public EventWindow(Event evnt, User user)
        {
            InitializeComponent();
            Loaded += Load_Window;

            this.evnt = evnt;
            this.user = user;
        }

        private void Load_Window(object sender, RoutedEventArgs e)
        {
            EventMainFrame.NavigationService.Navigate(new EventDetails(evnt, user));
        }
    }
}
