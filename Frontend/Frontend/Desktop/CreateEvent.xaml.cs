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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for CreateEvent.xaml
    /// </summary>
    public partial class CreateEvent : Page
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();
        private User user;

        public CreateEvent()
        {
            InitializeComponent();
            user = ((MainWindow)Application.Current.MainWindow).loggedIn;
        }

        private void btnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            if (
                tbEventName.Text.Length != 0 &&
                tbEventLocation.Text.Length != 0 &&
                tbEventDescription.Text.Length != 0 &&
                tbEventMinPrice.Text.Length != 0 &&
                tbEventMaxPrice.Text.Length != 0 &&
                tbEventNumOfParticipants.Text.Length != 0
                )
            {
                DateTime datetime = dtpEventDatetime.Value.Value;
                bool isPublic = rbEventPublic.IsChecked.Value;

                int numOfParticipants;
                Int32.TryParse(tbEventNumOfParticipants.Text, out numOfParticipants);

                int minPrice;
                Int32.TryParse(tbEventMinPrice.Text, out minPrice);

                int maxPrice;
                Int32.TryParse(tbEventMaxPrice.Text, out maxPrice);

                service.CreateEvent(
                    tbEventName.Text,
                    tbEventDescription.Text,
                    numOfParticipants,
                    minPrice,
                    maxPrice,
                    tbEventLocation.Text,
                    datetime,
                    isPublic,
                    user
                );
                MessageBox.Show("Event oprettet");
            } else
            {
                MessageBox.Show("Oplysninger mangler");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
