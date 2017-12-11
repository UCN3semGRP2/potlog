using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;

namespace PotLogServiceTests
{
    [TestClass]
    public class AddItemToEventTests
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
        public void AddItemToCategoryHappyDays()
        {
            //Arange
            var evnt = service.CreateEvent("Testing Item", "Testing Desc", 50, 50, 100, "Test Location", DateTime.Now.AddHours(2), true, this.User);
            service.AddCategoryToEvent(evnt.Id, "Testing Top Cat", "Testing Top Cat", null);

            var foundEvent = service.FindEventById(evnt.Id);
            var topCat = foundEvent.Components[0];

            service.AddItemToCategory(evnt.Id, topCat.Id, 5, "Testing Item Title", "Testing Item Description");
            var newFoundEvent = service.FindEventById(evnt.Id);

            var foundItem = service.FindComponentByParentId(topCat.Id);

            Assert.IsTrue(true);
            
        }
    }
}
