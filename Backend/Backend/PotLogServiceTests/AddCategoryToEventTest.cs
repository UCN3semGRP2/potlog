using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;
using System.Linq;
using System.ServiceModel;

namespace PotLogServiceTests
{
    [TestClass]
    public class AddCategoryToEventTest
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        public User User { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var email = "t@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            service.CreateUser("TestCreateEventUser", "Test", email, pw);
            this.User = service.LogIn(email, pw);

            
        }

        [TestMethod]
        public void TestAddCategoryToEventHappyDays()
        {
            var evnt = service.CreateEvent("test event", "test event please ignore", 5, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, this.User);

            var title = "Cat Title";
            var description = "Cat Description";
            service.AddCategoryToEvent(evnt.Id, title, description, null);

            var foundEvnt = service.FindEventById(evnt.Id);

            Assert.IsNotNull(foundEvnt.Components);
            Assert.AreEqual(foundEvnt.Components.Length, 1);
            var cat = foundEvnt.Components[0];
            Assert.IsTrue(cat is Category);
            Assert.AreEqual(title, cat.Title);
            Assert.AreEqual(description, cat.Description);
        }

        [TestMethod]
        public void TestAddCategoryToEventNoTitle()
        {
            var title = "";
            var description = "Description with no title";
            var evnt = service.CreateEvent("test event", "test event please ignore", 5, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, this.User);

            try
            {
                service.AddCategoryToEvent(evnt.Id, title, description, null);
                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        [TestMethod]
        public void TestAddCategoryToEventNoDescription()
        {
            var title = "Title with no description";
            var description = "";
            var evnt = service.CreateEvent("test event", "test event please ignore", 5, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, this.User);

            try
            {
                service.AddCategoryToEvent(evnt.Id, title, description, null);
                Assert.Fail();
            }
            catch (FaultException)
            {
                
            }
        }
    }
}
