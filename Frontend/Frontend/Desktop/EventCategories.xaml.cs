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
        private User u;
        private Item currentItem; 

        public EventCategories(Event evnt, User usr)
        {
            InitializeComponent();
            e = evnt;
            u = usr;
            currentItem = null;
            UpdateTopComboBox();
        }

        private void UpdateTopComboBox()
        {
            this.e = service.FindEventById(this.e.Id);
            if (e.Components != null)
            {
                List<string> topCompTitles = new List<string>();
                foreach (var item in e.Components)
                {
                    if (item.Parent == null) topCompTitles.Add(item.Title);
                }

                cbTopLevel.ItemsSource = topCompTitles;
                cbTopLevel.IsEnabled = true;


            }
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
            cbLevelTwo.SelectedIndex = -1;
            UpdateSecondComboBox();
        }

        private void UpdateSecondComboBox()
        {
            cbLevelTwo.ItemsSource = null;
            string topCatName = cbTopLevel.SelectedItem.ToString();
            var topCat = (Category)this.e.Components.Where(c => c.Title == topCatName && c is Category).FirstOrDefault();
            var subComponents = service.FindComponentByParentId(topCat.Id);

            if (topCat != null)
            {
                if (subComponents != null && subComponents.Count() > 0)
                {
                    List<string> LevelTwoTitles = new List<string>();
                    foreach (var item in subComponents)
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

        private void btnAddCatLevelTwo_Click(object sender, RoutedEventArgs e)
        {
            string catName = InsertName("kategori");
            string catDesc = InsertDesc("kategori");

            string topCatName = cbTopLevel.SelectedItem.ToString();
            var topCat = (Category)this.e.Components.Where(c => c.Title == topCatName && c is Category).FirstOrDefault();

            if (!catName.Equals("") && !catDesc.Equals(""))
            {
                service.AddCategoryToEvent(this.e.Id, catName, catDesc, topCat);
                this.e = service.FindEventById(this.e.Id);
                UpdateSecondComboBox();
                MessageBox.Show("Kategorien er tilføjet til eventet");
            }
            else
            {
                MessageBox.Show("Tilføjelse af kategori anulleret.");
            }

        }

        private void AddCategory()
        {
            //TODO Refactor Click-events.
        }

        private void UpdateComboBoxN(int n)
        {
            //TODO CASES
            //switch (switch_on)
            //{
            //    default:
            //}
        }

        private void btnAddItemLevelTwo_Click(object sender, RoutedEventArgs e)
        {
            string itemTitle = InsertName("ret");
            string itemDescription = InsertDesc("ret");
            int amount = InsertAmount();

            string topCatName = cbTopLevel.SelectedItem.ToString();
            var topCat = (Category)this.e.Components.Where(c => c.Title == topCatName && c is Category).FirstOrDefault();

            if (!itemTitle.Equals("") && !itemDescription.Equals("") && amount > 0)
            {
                service.AddItemToCategory(this.e.Id, topCat.Id, amount, itemTitle, itemDescription);
                this.e = service.FindEventById(this.e.Id);
                UpdateSecondComboBox();
                MessageBox.Show("Retten er tilføjet til eventet");
            }
            else
            {
                MessageBox.Show("Tilføjelse af retten anulleret.");
            }

        }

        private int InsertAmount()
        {
            int amount = 0;

            try
            {

                Int32.TryParse(Interaction.InputBox(
                    String.Format("Indtast venligst mængden på den nye ret nedenfor."),
                    String.Format("Ny ret: Mængde"),
                    ""
                    ), out amount);

            }
            catch (Exception)
            {
                MessageBox.Show("Indtastede mængde skal angives som tal.");
                InsertAmount();
            }

            return amount;
        }

        private void cbLevelTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateThirdComboBox();
        }

        private void UpdateThirdComboBox()
        {
            if (cbLevelTwo.ItemsSource != null && cbLevelTwo.SelectedIndex != -1)
            {

                cbLevelThree.ItemsSource = null;
                string topCatName = cbTopLevel.SelectedItem.ToString();
                int topCatId = ((Category)this.e.Components
                    .Where(c => c.Title == topCatName && c is Category).FirstOrDefault()).Id;

                string subCatName = cbLevelTwo.SelectedItem.ToString();

                var lvlTwoCats = service.FindComponentByParentId(topCatId);
                var lvlTwoCat = (Category)lvlTwoCats.Where(x => x.Title == subCatName && x is Category).FirstOrDefault();

                if (lvlTwoCat != null)
                {
                    var lvlThreeCats = service.FindComponentByParentId(lvlTwoCat.Id);

                    if (lvlThreeCats != null && lvlThreeCats.Count() > 0)
                    {
                        List<string> LevelThreeTitles = new List<string>();
                        foreach (var item in lvlThreeCats)
                        {
                            LevelThreeTitles.Add(item.Title);
                        }

                        cbLevelThree.ItemsSource = LevelThreeTitles;
                        cbLevelThree.IsEnabled = true;
                    }
                    btnAddItemLevelThree.IsEnabled = true;

                    lblCatTitle.Content = "";
                    tbCatDescription.Text = "";
                    lblCatAmount.Content = "";
                    btnSignUpForItem.IsEnabled = false;


                }
                else
                {
                    var lvlTwoItem = (Item)lvlTwoCats.Where(x => x.Title == subCatName && x is Item).FirstOrDefault();
                    lblCatTitle.Content = lvlTwoItem.Title;
                    tbCatDescription.Text = lvlTwoItem.Description;
                    lblCatAmount.Content = lvlTwoItem.Amount;
                    btnAddItemLevelThree.IsEnabled = false;
                    cbLevelThree.IsEnabled = false;
                    cbLevelThree.SelectedIndex = -1;
                    currentItem = lvlTwoItem;
                    btnSignUpForItem.IsEnabled = true;
                }
            }
        }

        private void btnAddItemLevelThree_Click(object sender, RoutedEventArgs e)
        {
            string itemTitle = InsertName("ret");
            string itemDescription = InsertDesc("ret");
            int amount = InsertAmount();

            string topCatName = cbTopLevel.SelectedItem.ToString();
            int topCatId = ((Category)this.e.Components
                .Where(c => c.Title == topCatName && c is Category).FirstOrDefault()).Id;

            string subCatName = cbLevelTwo.SelectedItem.ToString();

            var lvlTwoCats = service.FindComponentByParentId(topCatId);
            var lvlTwoCat = (Category)lvlTwoCats.Where(x => x.Title == subCatName && x is Category).FirstOrDefault();

            if (!itemTitle.Equals("") && !itemDescription.Equals("") && amount > 0)
            {
                service.AddItemToCategory(this.e.Id, lvlTwoCat.Id, amount, itemTitle, itemDescription);
                this.e = service.FindEventById(this.e.Id);
                UpdateThirdComboBox();
                MessageBox.Show("Retten er tilføjet til eventet");
            }
            else
            {
                MessageBox.Show("Tilføjelse af retten anulleret.");
            }
        }

        private void cbLevelThree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItemInfoLevelThree();
        }

        private void UpdateItemInfoLevelThree()
        {
            if (cbLevelThree.SelectedIndex != -1)
            {
                string topCatName = cbTopLevel.SelectedItem.ToString();
                int topCatId = ((Category)this.e.Components
                    .Where(c => c.Title == topCatName && c is Category).FirstOrDefault()).Id;

                string subCatName = cbLevelTwo.SelectedItem.ToString();

                var lvlTwoCats = service.FindComponentByParentId(topCatId);
                var lvlTwoCat = (Category)lvlTwoCats.Where(x => x.Title == subCatName && x is Category).FirstOrDefault();

                string itemName = cbLevelThree.SelectedItem.ToString();
                var lvlThreeItems = service.FindComponentByParentId(lvlTwoCat.Id);
                var lvlThreeItem = (Item)lvlThreeItems.Where(x => x.Title == itemName && x is Item).FirstOrDefault();

                lblCatTitle.Content = lvlThreeItem.Title;
                tbCatDescription.Text = lvlThreeItem.Description;
                lblCatAmount.Content = lvlThreeItem.Amount;
                currentItem = lvlThreeItem;
                btnSignUpForItem.IsEnabled = true;
            }

        }

        private void btnSignUpForItem_Click(object sender, RoutedEventArgs e)
        {
            service.SignUpForItem(u.Email, currentItem.Id);
            MessageBox.Show("Du er nu tilmeldt til event, og medbringer: " + currentItem.Title + "." + "\nMedbring mad til " + currentItem.Amount.ToString() + " personer.");
        }
    }
}
