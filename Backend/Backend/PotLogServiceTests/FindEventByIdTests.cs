using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;
using System.Linq;

namespace PotLogServiceTests
{
    [TestClass]
    public class FindEventByIdTests
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        public User User { get; set; }
        public Event Evnt { get; set; }
        public int EventId { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var email = "t@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            service.CreateUser("TestCreateEventUser", "Test", email, pw);
            this.User = service.LogIn(email, pw);

            this.Evnt = service.CreateEvent("test event", "test event please ignore", 5, 10.0, 100.5, "here", DateTime.Now.AddDays(5), false, this.User);
            this.EventId = Evnt.Id;
        }

        [TestMethod]
        public void TestFindEventByIdHappyDay()
        {
            var e = service.FindEventById(EventId);

            Assert.AreEqual(Evnt.Id, e.Id);
            Assert.AreEqual(Evnt.Title, e.Title);
            Assert.AreEqual(Evnt.Description, e.Description);
            Assert.AreEqual(Evnt.Location, e.Location);
            //Assert.AreEqual(Evnt.Datetime, e.Datetime);
            Assert.AreEqual(Evnt.IsPublic, e.IsPublic);
            Assert.AreEqual(Evnt.NumOfParticipants, e.NumOfParticipants);
            Assert.AreEqual(Evnt.PriceFrom, e.PriceFrom);
            Assert.AreEqual(Evnt.PriceTo, e.PriceTo);
        }

        [TestMethod]
        public void TestFindEvent()
        {
            var e = service.FindEventById(EventId);
            var regs = e.Registrations;

            bool UserIsAdmin = regs
                .Select(reg => reg.Event)
                .Where(ev => ev.Admin.Id == User.Id)
                .Select(x => x.Admin.Id)
                .Contains(User.Id);
            Assert.IsTrue(UserIsAdmin, "User is admin");

            foreach (var reg in regs)
            {
                Assert.AreEqual(Evnt.Id, reg.Event.Id);
            }

            
        }
    }
}
