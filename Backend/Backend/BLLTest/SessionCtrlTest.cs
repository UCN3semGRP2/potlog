using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using Model;

namespace BLLTest
{
    [TestClass]
    public class SessionCtrlTest
    {
        [TestMethod]
        public void TestIsValidated()
        {
            // Arrange
            SessionCtrl sCtrl = new SessionCtrl();
            DateTime dt = DateTime.Now.AddMinutes(4 * 60);
            Session s = new Session();
            s.ExpireDate = dt;

            // Act
            bool isValid = sCtrl.IsValidated(s);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void TestIsNotValidated()
        {
            // Arrange
            SessionCtrl sCtrl = new SessionCtrl();
            DateTime dt = Convert.ToDateTime("2010-12-25 16:58:00");
            Session s = new Session();
            s.ExpireDate = dt;

            // Act
            bool isValid = sCtrl.IsValidated(s);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
