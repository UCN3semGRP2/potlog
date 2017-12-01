using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using Model;

namespace BLLTest
{
    [TestClass]
    public class ComponentCtrlTest
    {
        [TestMethod]
        public void TestCreateCategory()
        {
            // Arrange
            ComponentCtrl cCtrl = new ComponentCtrl();
            string title = "Category name";
            string description = "Category description";

            // Act
            var cat = cCtrl.CreateCategory(title, description);

            // Assert
            var foundCategory = cCtrl.FindCategoryById(cat.Id);
            Assert.IsTrue(foundCategory.Title == title);
            Assert.IsTrue(foundCategory.Description == description);
        }

        [TestMethod]
        public void TestAddItem()
        {
            // Arrange
            EventCtrl eCtrl = new EventCtrl();
            string title = "E Title";
            string description = "E Desc";
            int numOfParticipants = 42;
            double priceFrom = 42;
            double priceTo = 42;
            string location = "E Location";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = true;
            User admin = null;

            ComponentCtrl cCtrl = new ComponentCtrl();
            string compTitle = "Cat Name";
            string compDescription = "Cat Desc";
            string itemTitle = "Item Name";
            string itemDescription = "Item Desc";
            int itemAmount = 42;

            // Act
            var evnt = eCtrl.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);
            var category = cCtrl.CreateCategory(compTitle, compDescription);
            eCtrl.AddCategory(evnt, category);
            var item = cCtrl.CreateItem(itemTitle, itemDescription, itemAmount);
            eCtrl.AddItem(evnt, category, item);

            // Assert
            var foundCategory = cCtrl.FindCategoryById(category.Id);
            Assert.IsNotNull(foundCategory.Components.Find(i => i.Id == item.Id));
            Assert.IsTrue(foundCategory.Components.Count > 0);


        }
    }
}
