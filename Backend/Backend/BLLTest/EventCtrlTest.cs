using BLL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTest
{
    [TestClass]
    public class EventCtrlTest
    {
        [TestMethod]
        public void TestCreateEvent()
        {
            var ctrl = new EventCtrl();

            Event e = new Event
            {
                Title = "test title" + Guid.NewGuid(),
                Description = "This is a long description\ncontaining newlines",
                NumOfParticipants = 5,
                PriceFrom = 100.0,
                PriceTo = 200.0,
                Location = "Sofiendalsvej 60",
                Datetime = DateTime.Now.AddHours(1), //+1 hour from now to not trigger the past date exception
                IsPublic = true
            };

            // Act
            Event output = ctrl.CreateEvent(e.Title, e.Description, e.NumOfParticipants, e.PriceFrom, e.PriceTo, e.Location, e.Datetime, e.IsPublic, null);


            // Assert
            Assert.AreEqual(e.Title, output.Title);

        }
        [TestMethod]
        public void TestRegisterToEvent()
        {
            // Arrange
            var ctrl = new EventCtrl();
            var uCtrl = new UserCtrl();

            Event e = new Event
            {
                Title = "test title" + Guid.NewGuid(),
                Description = "This is a long description\ncontaining newlines",
                NumOfParticipants = 5,
                PriceFrom = 100.0,
                PriceTo = 200.0,
                Location = "Sofiendalsvej 60",
                Datetime = DateTime.Now.AddHours(1), //+1 hour from now to not trigger the past date exception
                IsPublic = true
            };
            var user = uCtrl.CreateUser("efrgfvd", "fss", "sdf@sdf.dk" + Guid.NewGuid(), "dsasdc");
            Event newEvent = ctrl.CreateEvent(e.Title, e.Description, e.NumOfParticipants, e.PriceFrom, e.PriceTo, e.Location, e.Datetime, e.IsPublic, user);

            // Act
            ctrl.RegisterToEvent(newEvent, user);

            // Assert
            using (var ctx = new DAL.DALContext())
            {
                user = uCtrl.FindByEmail(user.Email);
                newEvent = ctrl.FindById(newEvent.Id);
                var reg = user.Registrations[0];

                Assert.AreEqual(reg.User, user);
                Assert.AreEqual(reg.Event.Id, newEvent.Id);
                Assert.IsTrue(user.Registrations.Contains(reg));

                Assert.IsTrue(newEvent.Registrations.Exists(r => r.Id == reg.Id));
            }
        }

        [TestMethod]
        public void TestSignUpForEvent()
        {
            var user = new UserCtrl().CreateUser("1", "2", "test@test.test" + Guid.NewGuid(), "1234");
            var eCtrl = new EventCtrl();
            var e = eCtrl.CreateEvent("dsd", "dewdc", 23, 213.3, 21312.3, "here", DateTime.Now, false, user);


            // Act
            eCtrl.SignUpForEvent(user.Email, e.Id);

            // Assert
            using (var ctx = new DALContext())
            {
                var reg = new UserCtrl().FindByEmail(user.Email).Registrations[0];

                var found = ctx.Registrations.Find(reg.Id);
                Assert.AreEqual(reg.Id, found.Id);
            }

        }

        [TestMethod]
        public void TestAddComponent()
        {
            // Arrange
            EventCtrl eCtrl = new EventCtrl();
            var e = eCtrl.CreateEvent("Event", "Evently event",
                2, 20, 100, "Right here", DateTime.Now, true, null);
            Category c = new ComponentCtrl().CreateCategory("Cat", "CateCat", null);

            //Act
            eCtrl.AddCategory(e, c);

            //Assert
            var foundEvent = eCtrl.FindById(e.Id);
            Assert.IsTrue(foundEvent.Components.Count == 1);
            var foundComponent = foundEvent.Components.First();
            Assert.IsTrue(c.Id == foundComponent.Id);
            Assert.IsTrue(foundComponent is Category); //is returns true if an instance is in the inheritance tree
        }

        [TestMethod]
        public void TestAddCategoryToCategory()
        {
            // Arrange
            EventCtrl eCtrl = new EventCtrl();
            ComponentCtrl cCtrl = new ComponentCtrl();
            UserCtrl uCtrl = new UserCtrl();
            var u = uCtrl.CreateUser("Test User", "Test User", "test@email.com", "password");
            var e = eCtrl.CreateEvent("Testing Event", "Test", 2, 20, 100, "Right here", DateTime.Now.AddHours(5), true, u);
            var c1 = cCtrl.CreateCategory("Testing Category Lvl 1", "Test", null);
            var c2 = cCtrl.CreateCategory("Testing Category Lvl 2", "Test", c1);

            // Act
            eCtrl.AddCategory(e, c1);
            eCtrl.AddCategory(e, c2);

            Assert.IsTrue(true);



        }


        [TestMethod]
        public void TestAddItem()
        {


            // Arrange
            ComponentCtrl cCtrl = new ComponentCtrl();
            EventCtrl eCtrl = new EventCtrl();
            // Act
            var evnt = eCtrl.CreateEvent("E Title", "E Desc", 42, 42, 42, "E Location", DateTime.Now.AddDays(5), true, null);
            var category = cCtrl.CreateCategory("Cat Name", "Cat desc", null);
            eCtrl.AddCategory(evnt, category);
            //var category2 = cCtrl.CreateCategory("Cat2 Name2", "Cat2 desc2", category);
            //eCtrl.AddCategory(evnt, category2);
            var item = cCtrl.CreateItem("Item Name", "Item Desc", 42, category);
            eCtrl.AddItem(evnt, category, item);

            // Assert
            var foundCategory = cCtrl.FindCategoryById(category.Id);
            var foundItem = ((Item)((Category)evnt.Components[0]).Components[0]);
            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundCategory.Components.Count == 1);
            Assert.IsTrue(foundCategory.Components[0].Id == foundItem.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), 
            "PriceFrom is NOT smaller than priceTo")]
        public void TestPriceFromIsLargerThanPriceTo()
        {
            // Arrange
            EventCtrl eCtrl = new EventCtrl();
            double priceFrom = 100;
            double priceTo = 50;
            // Act
            var e = eCtrl.CreateEvent("Event", "Evently event",
               2, priceFrom, priceTo, "Right here", DateTime.Now, true, null);

            // Assert
            Assert.Fail();
        }
    }
}
