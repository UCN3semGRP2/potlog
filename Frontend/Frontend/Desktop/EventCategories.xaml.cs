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
    /// Interaction logic for EventCategories.xaml
    /// </summary>
    public partial class EventCategories : Page
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();
        private Event e;
        public EventCategories(Event evnt)
        {
            InitializeComponent();
            e = evnt;
            FillTopComboBoxes();
        }

        private void FillTopComboBoxes()
        {
            if (e.Components.Count() > 0)
            {
                cbTopLevel.IsEnabled = true;

                foreach (var item in e.Components)
                {
                    cbTopLevel.Items.Add(item);
                }
            }

            //TODO Level 2 and Level 3.
        }

        private void btnAddTopLevel_Click(object sender, RoutedEventArgs e)
        {
            string catName = Interaction.InputBox(
                "Indtast venligst navnet på kategorien nedenfor.",
                "Ny Kategori: Navn",
                ""
                );

            string catDesc = Interaction.InputBox(
                "Indtast venligst en beskrivende tekst om kategorien nedenfor.",
                "Ny Kategori: Beskrivelse",
                ""
                );

            if (!catName.Equals("") && !catDesc.Equals(""))
            {
                service.AddCategoryToEvent(this.e.Id, catName, catDesc, null);
                MessageBox.Show("Kategorien er tilføjet til eventet");
            }
            else
            {
                MessageBox.Show("Tilføjelse af kategori anulleret.");
            }
        }
    }
}
