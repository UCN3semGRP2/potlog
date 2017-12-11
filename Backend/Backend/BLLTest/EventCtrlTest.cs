using BLL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using PotLogService;
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
            UserCtrl uCtrl = new UserCtrl();
            var u = uCtrl.CreateUser("Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");


            Event e = new Event
            {
                Title = "test title" + Guid.NewGuid(),
                Description = "This is a long description\ncontaining newlines",
                NumOfParticipants = 5,
                PriceFrom = 100.0,
                PriceTo = 200.0,
                Location = "Sofiendalsvej 60",
                Datetime = DateTime.Now.AddHours(1), //+1 hour from now to not trigger the past date exception
                IsPublic = true,
                Admin = u
            };

            // Act
            Event output = ctrl.CreateEvent(e.Title, e.Description, e.NumOfParticipants, e.PriceFrom, e.PriceTo, e.Location, e.Datetime, e.IsPublic, u);


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
            var user = new UserCtrl().CreateUser("Fornavn", "Efternavn", "test@test.test" + Guid.NewGuid(), "123456");
            var eCtrl = new EventCtrl();
            var e = eCtrl.CreateEvent("dsd", "dewdc", 23, 213.3, 21312.3, "here", DateTime.Now.AddHours(5), false, user);


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
        public void TestSignUpForEventHappyDaysBLL()
        {
            var email = "t@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            new UserCtrl().CreateUser("TestCreateEventUser", "Test", email, pw);
            var User = new UserCtrl().LogIn(email, pw);

            var Evnt = new EventCtrl().CreateEvent("test event", "test event please ignore", 5, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, User);
            var EventId = Evnt.Id;

            new EventCtrl().SignUpForEvent(User.Email, EventId);

            Evnt = new EventCtrl().FindById(EventId);

            var registeredUsers = Evnt.Registrations.Select(x => x.User).ToList();
            Assert.AreEqual(1, registeredUsers.Count);
            var u = registeredUsers[0];
            Assert.AreEqual(User.Id, u.Id);

            Assert.IsTrue(new UserCtrl().IsRegisteredToEvent(User, Evnt), "User is registred using the isRegisteredToEvent method");
        }

        [TestMethod]
        public void TestAddComponent()
        {
            // Arrange
            EventCtrl eCtrl = new EventCtrl();
            UserCtrl uCtrl = new UserCtrl();
            var u = uCtrl.CreateUser("Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");

            var e = eCtrl.CreateEvent("Event", "Evently event",
                2, 20, 100, "Right here", DateTime.Now, true, u);
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
            var u = uCtrl.CreateUser("Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");
            var e = eCtrl.CreateEvent("Testing Event", "Test", 2, 20, 100, "Right here", DateTime.Now.AddHours(5), true, u);
            var c1 = cCtrl.CreateCategory("Testing Category Lvl 1", "Test", null);

            // Act
            eCtrl.AddCategory(e, c1);

            var c2 = cCtrl.CreateCategory("Testing Category Lvl 2", "Test", c1);
            var e2 = eCtrl.FindById(e.Id);
            eCtrl.AddCategory(e2, c2);

            Assert.IsTrue(c2.Parent.Id == c1.Id);
            Assert.IsTrue(c2.EventId == e.Id);

        }

        [TestMethod]
        public void SystemTestAddCategoryToCategory()
        {
            EventCtrl eCtrl = new EventCtrl();
            ComponentCtrl cCtrl = new ComponentCtrl();
            UserCtrl uCtrl = new UserCtrl();
            IService service = new Service();

            User u = uCtrl.CreateUser("System Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");
            Event e = service.CreateEvent("System Test Event", "Test", 2, 20, 100, "Right here", DateTime.Now.AddHours(5), true, u);
            
            service.AddCategoryToEvent(e.Id, "System Test Cat 1", "Cat 1", null);
            e = service.FindEventById(e.Id);

            service.AddCategoryToEvent(e.Id, "System Test Cat 2", "Cat 2",
                e.Components.Where(c => c.Title == "System Test Cat 1" && c is Category).FirstOrDefault());

            Assert.IsTrue(true);

        }

        [TestMethod]
        public void TestAddItem()
        {


            // Arrange
            ComponentCtrl cCtrl = new ComponentCtrl();
            EventCtrl eCtrl = new EventCtrl();
            UserCtrl uCtrl = new UserCtrl();
            var u = uCtrl.CreateUser("Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");

            // Act
            var evnt = eCtrl.CreateEvent("E Title", "E Desc", 42, 42, 42, "E Location", DateTime.Now.AddDays(5), true, u);
            var category = cCtrl.CreateCategory("Cat Name", "Cat desc", null);
            eCtrl.AddCategory(evnt, category);
            var category2 = cCtrl.CreateCategory("Cat2 Name2", "Cat2 desc2", category);
            eCtrl.AddCategory(evnt, category2);
            var item = cCtrl.CreateItem("Item Name", "Item Desc", 42, category2);
            eCtrl.AddItem(evnt, category2, item);

            // Assert
            var foundCategory = cCtrl.FindCategoryById(category2.Id);
            var foundItem = ((Item)((Category)evnt.Components[1]).Components[0]);
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
