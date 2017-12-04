using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;
using System.Linq;

namespace PotLogServiceTests
{
    [TestClass]
    public class AddCategoryToEventTest
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        public User User { get; set; }
        public Event Evnt { get; set; }
        public int EventId { get; set; }
        public Component Cat { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.Evnt = service.CreateEvent("test event", "test event please ignore", 5, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, this.User);
            this.EventId = Evnt.Id;
        }

        [TestMethod]
        public void TestAddCategoryToEventHappyDays()
        {
            service.AddCategoryToEvent(Evnt.Id, "Cat Title", "Cat Description");
            Category c = Evnt.Components.Where(x => x.Title == "Cat Title" && x is Category).FirstOrDefault() as Category;
            Assert.IsTrue(c.EventId == Evnt.Id);
        }
    }
}
