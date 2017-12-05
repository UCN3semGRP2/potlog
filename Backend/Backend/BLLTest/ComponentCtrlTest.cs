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
            var cat = cCtrl.CreateCategory(title, description, null);

            // Assert
            var foundCategory = cCtrl.FindCategoryById(cat.Id);
            Assert.IsTrue(foundCategory.Title == title);
            Assert.IsTrue(foundCategory.Description == description);
        }
    }
}
