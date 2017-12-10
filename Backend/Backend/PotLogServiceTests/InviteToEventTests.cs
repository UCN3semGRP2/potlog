using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PotLogServiceTests
{
    [TestClass]
    public class InviteToEventTests
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        [TestMethod]
        public void TestInviteToEventHappyDay()
        {
            // Create admin user
            var adminEmail = "admin@admin." + Guid.NewGuid();
            var adminPw = "123456";
            service.CreateUser("Admin", "adminuser", adminEmail, adminPw);
            var admin = service.LogIn(adminEmail, adminPw);

            // Admin creates an event
            var evnt = service.CreateEvent("e", "its an event", 5, 10, 100, "here", DateTime.Now.AddDays(5), false, admin);

            // We create a new normal user
            var userEmail = "user@user." + Guid.NewGuid();
            var userPw = "123456";
            service.CreateUser("user", "userson", userEmail, userPw);

            // Admin invites the normal user
            string inviteString = service.GetInviteString(evnt, admin);


            // The normal user logs in and accepts
            var usr = service.LogIn(userEmail, userPw);
            service.AcceptInviteString(usr, inviteString);
        }
    }
}
