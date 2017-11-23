using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BLL;
using DAL;

namespace BLLTest
{
    /// <summary>
    /// Summary description for UnitOfWorkTests
    /// </summary>
    [TestClass]
    public class UnitOfWorkTests
    {


        [TestMethod]
        public void TestSignUpForEvent()
        {
            var UoW = new UnitOfWork();
            using (var ctx = new DALContext())
            {
                var user = new UserCtrl().CreateUser(ctx, "1", "2", "test@test.test"+Guid.NewGuid(), "1234");
                var e = new EventCtrl().CreateEvent(ctx, "dsd", "dewdc", 23, 213.3, 21312.3, "here", DateTime.Now, false, user);
                ctx.SaveChanges();


                // Act
                var reg = UoW.SignUpForEvent(user.Email, e.Id);

                // Assert
                var found = ctx.Registrations.Find(reg.Id);
                Assert.AreEqual(reg.Id, found.Id);
            }
            
        }
    }
}
