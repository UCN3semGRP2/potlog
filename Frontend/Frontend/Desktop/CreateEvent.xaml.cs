using Desktop.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

    public partial class CreateEvent : Page
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();
        private User user;

        public CreateEvent()
        {
            InitializeComponent();
            user = ((MainWindow)Application.Current.MainWindow).loggedIn;
            user = service.UpdateUserInfo(user);
            rbEventPrivate.IsChecked = true;
        }

        private void btnCreateEvent_Click(object sender, RoutedEventArgs e)
        {

            if (
                tbEventName.Text.Length != 0 &&
                tbEventLocation.Text.Length != 0 &&
                tbEventDescription.Text.Length != 0 &&
                ValidateHelper.checkStringForNumbers(tbEventMinPrice.Text) &&
                ValidateHelper.checkStringForNumbers(tbEventNumOfParticipants.Text) &&
                ValidateHelper.checkStringForNumbers(tbEventMaxPrice.Text)
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

                try
                {
                    var evnt = service.CreateEvent(
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

                    var eventWindow = new EventWindow(evnt, user);
                    eventWindow.Show();
                    this.NavigationService.Navigate(new MainPage());
                }
                catch (FaultException err)
                {
                    MessageBox.Show(err.Message);
                    return;
                }
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
            ValidateHelper.checkNumberTooltip(tbEventNumOfParticipants);
        }

        private void tbEventMinPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateHelper.checkNumberTooltip(tbEventMinPrice);
        }

        private void tbEventMaxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateHelper.checkNumberTooltip(tbEventMaxPrice);
        }


    }
}
