using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;
using System.Collections.Generic;

namespace PotLogServiceTests
{
    [TestClass]
    public class IsRegisteredToEventTests
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
        public void IsRegisteredToEventTest()
        {
            List<string> userEmails = new List<string>();
            //Create 10 users 
            int nRegistered = 0;
            for (int i = 0; i < 10; i++)
            {
                var email = "t@t.t" + Guid.NewGuid();
                var pw = "hunter1";
                service.CreateUser("TestCreateEventUser", "Test", email, pw);
                userEmails.Add(email);

                var u = service.LogIn(email, pw);
                if (i % 2 == 0)
                {
                    service.SignUpForEvent(email, EventId);
                    nRegistered++;
                    bool isRegistered = service.IsRegisteredToEvent(u, Evnt);
                    Assert.IsTrue(isRegistered);
                }
                else
                {
                    bool isRegistered = service.IsRegisteredToEvent(u, Evnt);
                    Assert.IsFalse(isRegistered);
                }
            }

            var e = service.FindEventById(EventId);
            Assert.AreEqual(e.Registrations.Length, nRegistered);
        }
    }
}
