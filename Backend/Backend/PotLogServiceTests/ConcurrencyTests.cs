using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;

namespace PotLogServiceTests
{
    [TestClass]
    public class ConcurrencyTests
    {

        [TestMethod]
        public void TestTwoUsersSignUpWhenOnlyOneMoreParticipantAvailable()
        {
            // We repeat the test to because we cannot guarantee that the threads are run in the same order on each run
            for (int i = 0; i < 5; i++)
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

                Assert.AreNotEqual(u1SignedUp, u2SignedUp, string.Format("User1 and User2 have the same signedup state {0} and {1} after {2} runs", u1SignedUp, u2SignedUp, i));

                bool atLeastOneSignedUp = u1SignedUp || u2SignedUp;
                Assert.IsTrue(atLeastOneSignedUp, string.Format("At least one user should be signed up, but they are not after {0} runs", i));
            }
        }

        [TestMethod]
        public void TestTwoUsersSignUpWithTheSameMail()
        {
            for (int i = 0; i < 10; i++)
            {
                var commonMail = "dup@dup." + Guid.NewGuid();

                var service1 = new ServiceReference.ServiceClient();
                var service2 = new ServiceReference.ServiceClient();

                var u1Created = false;
                var u2Created = false;

                var t1 = new Thread(() =>
                {
                    service1.CreateUser("Dupuser", "Dupson", commonMail, "123456");
                    u1Created = true;
                });

                var t2 = new Thread(() =>
                {
                    service1.CreateUser("Dupuser", "Dupson", commonMail, "123456");
                    u2Created = true;
                });

                t1.Start();
                t2.Start();

                t1.Join();
                t2.Join();

                Assert.AreNotEqual(u1Created, u2Created, string.Format("User1 and User2 have the same created state"));

                bool atleastOneWasCreated = u1Created || u2Created;
                Assert.IsTrue(atleastOneWasCreated, "Atleast one of the users should have been created");

            }

        }

        [TestMethod]
        public void TestSignUpForSameItemConcurrent()
        {
            for (int i = 0; i < 10; i++)
            {
                var mainService = new ServiceReference.ServiceClient();

                var adminEmail = "admin@admin.admin" + Guid.NewGuid();
                var adminPw = "123456";
                mainService.CreateUser("admin", "Adminson", adminEmail, adminPw);
                var admin = mainService.LogIn(adminEmail, adminPw);

                var evnt = mainService.CreateEvent("main event", "the event", 1000, 5.5, 100.5, "here", DateTime.Now.AddDays(1), false, admin);

                var inviteString = mainService.GetInviteString(evnt, admin);

                var user1Mail = "user@1." + Guid.NewGuid();
                var user1Pw = "123456";
                mainService.CreateUser("user1", "userson", user1Mail, user1Pw);

                var user2Mail = "user@2." + Guid.NewGuid();
                var user2Pw = "123456";
                mainService.CreateUser("user2", "userson", user2Mail, user2Pw);

                ServiceReference.IService service1 = new ServiceReference.ServiceClient();
                var u1 = service1.LogIn(user1Mail, user1Pw);
                var u1Event = service1.AcceptInviteString(u1, inviteString);

                ServiceReference.IService service2 = new ServiceReference.ServiceClient();
                var u2 = service2.LogIn(user2Mail, user2Pw);
                var u2Event = service1.AcceptInviteString(u2, inviteString);


                mainService.AddCategoryToEvent(evnt.Id, "Main cat", "The main cat", null);
                evnt = mainService.FindEventById(evnt.Id);
                var cat = (Category)evnt.Components[0];
                mainService.AddItemToCategory(evnt.Id, cat.Id, 10, "item", "THE item");


                bool u1SignedUpForItem = false;
                bool u2SignedUpForItem = false;

                var t1 = new Thread(() =>
                {
                    u1Event = service1.FindEventById(u1Event.Id);
                    var itemId = service1.FindComponentByParentId(cat.Id)[0].Id;
                    service1.SignUpForItem(u1.Email, itemId);

                    u1SignedUpForItem = true;
                });

                var t2 = new Thread(() =>
                {
                    u2Event = service2.FindEventById(u2Event.Id);
                    var itemId = service2.FindComponentByParentId(cat.Id)[0].Id;
                    service2.SignUpForItem(u2.Email, itemId);

                    u2SignedUpForItem = true;
                });

                t1.Start();
                t2.Start();

                t1.Join();
                t2.Join();

                Assert.AreNotEqual(u1SignedUpForItem, u2SignedUpForItem, string.Format("User1 and User2 have the same signedup state {0} and {1} after {2} runs", u1SignedUpForItem, u2SignedUpForItem, i));

                bool atLeastOneSignedUp = u1SignedUpForItem || u2SignedUpForItem;
                Assert.IsTrue(atLeastOneSignedUp, string.Format("At least one user should be signed up, but they are not after {0} runs", i));
            }
        }
    }
}
