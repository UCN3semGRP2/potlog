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
            UpdateTopComboBox();
        }

        private void UpdateTopComboBox()
        {
            if (e.Components != null)
            {
                List<string> topCompTitles = new List<string>();
                foreach (var item in e.Components)
                { 
                    topCompTitles.Add(item.Title);
                }

                cbTopLevel.ItemsSource = topCompTitles;
                cbTopLevel.IsEnabled = true;
            }

            //TODO Level 2 and Level 3.
        }

        private void btnAddCatTopLevel_Click(object sender, RoutedEventArgs e)
        {
            string catName = InsertName("kategori");
            string catDesc = InsertDesc("kategori");

            if (!catName.Equals("") && !catDesc.Equals(""))
            {
                service.AddCategoryToEvent(this.e.Id, catName, catDesc, null);
                this.e = service.FindEventById(this.e.Id);
                UpdateTopComboBox();
                MessageBox.Show("Kategorien er tilføjet til eventet");
            }
            else
            {
                MessageBox.Show("Tilføjelse af kategori anulleret.");
            }
        }
        private string InsertName(string catOrItem)
        {
            return Interaction.InputBox(
                String.Format("Indtast venligst navnet på den nye {0} nedenfor.", catOrItem),
                String.Format("Ny {0}: Navn", catOrItem),
                ""
                );
        }
        private string InsertDesc(string catOrItem)
        {
            return Interaction.InputBox(
                String.Format("Indtast venligst en beskrivende tekst om den nye {0} nedenfor.", catOrItem),
                String.Format("Ny {0}: Beskrivelse", catOrItem),
                ""
                );
        }

        private void cbTopLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSecondLevel();
        }

        private void UpdateSecondLevel()
        {
            string topCatName = cbTopLevel.SelectedItem.ToString();
            var topCat = (Category)this.e.Components.Where(c => c.Title == topCatName && c is Category).FirstOrDefault();

            if (topCat != null)
            {
                if (topCat.Components != null)
                {
                    List<string> LevelTwoTitles = new List<string>();
                    foreach (var item in topCat.Components)
                    {
                        LevelTwoTitles.Add(item.Title);
                    }

                    cbLevelTwo.ItemsSource = LevelTwoTitles;
                    cbLevelTwo.IsEnabled = true;
                }

                btnAddCatLevelTwo.IsEnabled = true;
                btnAddItemLevelTwo.IsEnabled = true;
            }
        }
    }
}
