using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;
using System.Linq;
using System.ServiceModel;

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

            this.Evnt = service.CreateEvent("test event", "test event please ignore", 100, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, this.Admin);
            this.EventId = Evnt.Id;

            service.AddCategoryToEvent(EventId, "Test Category", "Test Category", null);
            this.Evnt = service.FindEventById(EventId);
            service.AddItemToCategory(EventId, Evnt.Components[0].Id, 5, "Test Item", "Test Item");
            this.Evnt = service.FindEventById(EventId);
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
            service.SignUpForEvent(User.Email, EventId);

            User = service.UpdateUserInfo(User);
            Evnt = service.FindEventById(EventId);

            var comp = Evnt.Components[0];
            var subcomps = service.FindComponentByParentId(comp.Id);
            int itemId = subcomps[0].Id;

            service.SignUpForItem(User.Email, itemId);

            Evnt = service.FindEventById(EventId);
            bool userIsRegistered = Evnt
                .Registrations
                .Select(x => x.User)
                .Select(x => x.Id)
                .Contains(User.Id);

            Registration reg = Evnt
                .Registrations
                .Where(x => x.User.Id == User.Id)
                .Single();

            Item i = reg.Items[0];

            Assert.IsTrue(userIsRegistered, "user is registered on returned event");
            Assert.AreEqual(subcomps[0].Id, i.Id);
        }

        [TestMethod]
        public void TestDuplicateSignUpForEvent()
        {
            var email = "user@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            service.CreateUser("TestCreateEventUser", "Test", email, pw);
            var User = service.LogIn(email, pw);

            // First time should be OK
            service.SignUpForEvent(email, EventId);

            // Now we sign up again. This should fail
            try
            {
                service.SignUpForEvent(email, EventId);
                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }
    }
}
