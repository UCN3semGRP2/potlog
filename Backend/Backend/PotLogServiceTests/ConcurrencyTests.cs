using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PotLogServiceTests
{
    [TestClass]
    public class ConcurrencyTests
    {
        static readonly object locker = new object();

        [TestMethod]
        public void TestTwoUsersSignUpWhenOnlyOneMoreParticipantAvailable()
        {
            var numParticipants = 2; // Two participants as we have the admin and then two users try to participate at the same time making one of them unable to sign up
            var mainService = new ServiceReference.ServiceClient();

            var adminEmail = "admin@admin.admin" + Guid.NewGuid();
            var adminPw = "123456";
            mainService.CreateUser("admin", "Adminson", adminEmail, adminPw);
            var admin = mainService.LogIn(adminEmail, adminPw);

            var evnt = mainService.CreateEvent("main event", "the event", numParticipants, 5.5, 100.5, "here", DateTime.Now.AddDays(1), false, admin);

            var inviteString = mainService.GetInviteString(evnt, admin);

            var user1Mail = "user@1." + Guid.NewGuid();
            var user1Pw = "123456";
            mainService.CreateUser("user1", "userson", user1Mail, user1Pw);

            var user2Mail = "user@2." + Guid.NewGuid();
            var user2Pw = "123456";
            mainService.CreateUser("user2", "userson", user2Mail, user2Pw);

            ServiceReference.IService service1 = new ServiceReference.ServiceClient();
            var u1 = service1.LogIn(user1Mail, user1Pw);
            bool u1SignedUp = false;

            ServiceReference.IService service2 = new ServiceReference.ServiceClient();
            var u2 = service2.LogIn(user2Mail, user2Pw);
            bool u2SignedUp = false;

            var t1 = new Thread(() =>
            {
                var u1Event = service1.AcceptInviteString(u1, inviteString);
                u1SignedUp = u1Event != null;
            });

            var t2 = new Thread(() =>
            {
                var u2Event = service2.AcceptInviteString(u2, inviteString);
                u2SignedUp = u2Event != null;
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Assert.AreNotEqual(u1SignedUp, u2SignedUp, string.Format("User1 and User2 have the same signedup state {0} and {1}", u1SignedUp, u2SignedUp));

            bool atLeastOneSignedUp = u1SignedUp || u2SignedUp;
            Assert.IsTrue(atLeastOneSignedUp, "At least one user should be signed up, but they are not");
        }
    }
}
