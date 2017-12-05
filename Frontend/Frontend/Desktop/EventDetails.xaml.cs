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
using Microsoft.VisualBasic;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for EventDetails.xaml
    /// </summary>
    public partial class EventDetails : Page
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();

        private bool isRegisteredToEvent = false;
        private bool isAdmin = false;

        private Event e;
        private User u;

        public EventDetails(Event e, User u)
        {
            InitializeComponent();
            rbEventPublic.IsEnabled = false;
            rbEventPrivate.IsEnabled = false;
            u = service.UpdateUserInfo(u);
            btnEventEdit.IsEnabled = false;
            isRegisteredToEvent = service.IsRegisteredToEvent(u, e);

            if (isAdmin && isRegisteredToEvent)
            {
                btnEventEdit.IsEnabled = true;
            }

            if (isRegisteredToEvent)
            {
                btnEventRegister.Visibility = Visibility.Hidden;
            }
            else
            {
                btnEventSeeParticipants.Visibility = Visibility.Hidden;
            }

            populateInfo(e);

            this.u = u;
            this.e = e;
        }

        private void populateInfo(Event e)
        {
            e = service.FindEventById(e.Id);

            lblEventName.Content = e.Title;
            lblEventLocation.Content = e.Location;
            lblEventDate.Content = e.Datetime.Date.ToString("dd/MM/yyyy");
            lblEventTime.Content = e.Datetime.ToString("HH:mm");
            rbEventPrivate.IsChecked = !e.IsPublic;
            rbEventPublic.IsChecked = e.IsPublic;
            tbEventDescription.Text = e.Description;

            var registrations = e.Registrations == null ? 0 : e.Registrations.Length;
            lblEventNumOfParticipants.Content = string.Format("{0} ud af {1} deltagere", registrations, e.NumOfParticipants);

        }

        private void btnEventRegister_Click(object sender, RoutedEventArgs e)
        {
            service.SignUpForEvent(u.Email, this.e.Id);

            MessageBox.Show(string.Format("Success registering oprettet"));

            //var nextPage = TODO
            //this.NavigationService.Navigate(nextPage);
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EventCategories(this.e));
        }
    }
}

