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
            ComponentCtrl cCtrl = new ComponentCtrl();
            string compTitle = "Cat Name";
            string compDescription = "Cat Desc";
            string itemTitle = "Item Name";
            string itemDescription = "Item Desc";
            int itemAmount = 42;

            // Act
            var category = cCtrl.CreateCategory(compTitle, compDescription);
            Item item = cCtrl.CreateItem(itemTitle, itemDescription, itemAmount, category);

            // Assert
            var foundCategory = cCtrl.FindCategoryById(category.Id);
            Assert.IsNotNull(foundCategory.Components.Find(i => i.Id == item.Id));
            Assert.IsTrue(foundCategory.Components.Count > 0);


        }
    }
}
