using Desktop.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            rbEventPrivate.IsChecked = true;
        }

        private void btnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            if (
                tbEventName.Text.Length != 0 &&
                tbEventLocation.Text.Length != 0 &&
                tbEventDescription.Text.Length != 0 &&
                checkStringForNumbers(tbEventMinPrice.Text) &&
                checkStringForNumbers(tbEventNumOfParticipants.Text) &&
                checkStringForNumbers(tbEventMaxPrice.Text)
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

        private void tbEventNumOfParticipants_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkStringForNumbers(tbEventNumOfParticipants.Text))
                {
                tbEventNumOfParticipants.ToolTip = null;
                tbEventNumOfParticipants.BorderBrush = Brushes.Black;
                
            }
            else
            {
                tbEventNumOfParticipants.BorderBrush = Brushes.Red;
                tbEventNumOfParticipants.ToolTip = "Der må kun skrives tal i dette felt";
            }
        }

        private void tbEventMinPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkStringForNumbers(tbEventMinPrice.Text))
            {
                tbEventMinPrice.ToolTip = null;
                tbEventMinPrice.BorderBrush = Brushes.Black;
            }
            else
            {
                tbEventMinPrice.BorderBrush = Brushes.Red;
                tbEventMinPrice.ToolTip = "Der må kun skrives tal i dette felt";
            }
        }

        private void tbEventMaxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkStringForNumbers(tbEventMaxPrice.Text))
            {
                tbEventMaxPrice.ToolTip = null;
                tbEventMaxPrice.BorderBrush = Brushes.Black;
            }
            else
            {
                tbEventMaxPrice.BorderBrush = Brushes.Red;
                tbEventMaxPrice.ToolTip = "Der må kun skrives tal i dette felt";
            }

        }

        private bool checkStringForNumbers(string testString)
        {
            var regex = new Regex("^[0-9]+$");
            return regex.IsMatch(testString);
        }
    }
}
