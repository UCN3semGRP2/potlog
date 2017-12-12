using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;
using System.Linq;

namespace PotLogServiceTests
{
    [TestClass]
    public class SignUpForEventTests
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        public User Admin { get; set; }
        public Event Evnt { get; set; }
        public int EventId { get; set; }
        

        [TestInitialize]
        public void TestInitialize()
        {
            var email = "t@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            service.CreateUser("TestCreateEventUser", "Test", email, pw);
            this.Admin = service.LogIn(email, pw);

            this.Evnt = service.CreateEvent("test event", "test event please ignore", 5, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, this.Admin);
            this.EventId = Evnt.Id;

            service.AddCategoryToEvent(EventId, "Test Category", "Test Category", null);
            this.Evnt = service.FindEventById(EventId);
            service.AddItemToCategory(EventId, Evnt.Components[0].Id, 5, "Test Item", "Test Item");

        }

        [TestMethod]
        public void TestSignUpForEventHappyDays()
        {
            var email = "user@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            service.CreateUser("TestCreateEventUser", "Test", email, pw);
            var User = service.LogIn(email, pw);

            service.SignUpForEvent(User.Email, EventId);

            Evnt = service.FindEventById(EventId);

            bool userIsRegistered = Evnt
                .Registrations
                .Select(x => x.User)
                .Select(x => x.Id)
                .Contains(User.Id);
            Assert.IsTrue(userIsRegistered, "user is registered on returned event");

            Assert.IsTrue(service.IsRegisteredToEvent(User, Evnt), "User is registred using the isRegisteredToEvent method");
        }

        [TestMethod]
        public void TestSignUpForItemHappyDays()
        {
            var email = "user@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            service.CreateUser("TestCreateEventUser", "Test", email, pw);
            var User = service.LogIn(email, pw);

            int ItemId = ((Category)(Evnt.Components[0])).Components[0].Id;

            service.SignUpForItem(User.Email, ItemId);
            Evnt = service.FindEventById(EventId);

            Registration reg = Evnt
                .Registrations
                .Where(x => x.User.Id == User.Id)
                .Where(x => x.Items.Select(i => i.Id).Contains(ItemId))
                .SingleOrDefault();

            Assert.IsNotNull(reg);
        }
    }
}
