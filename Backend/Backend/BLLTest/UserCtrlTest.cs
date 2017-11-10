using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace BLLTest
{
    [TestClass]
    public class UserCtrlTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestValidatePassword_withValidPw()
        {
            // Arrange
            var clearTextPW = "hunter1";
            var salt = BLL.HashingHelper.GenerateSalt();
            var u1PW = BLL.HashingHelper.HashPassword(clearTextPW, salt); 
            var u1 = new User { Id = 1, Email = "1@1.1", Firstname = "user1", Lastname = "1", Password=u1PW, Salt=salt };

            var ctrl = new BLL.UserCtrl();
            // Act
            bool isValid = ctrl.ValidatePassword(u1, clearTextPW);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void TestValidatePassword_withInValidPw()
        {
            // Arrange
            var clearTextPW = "hunter1";
            var salt = BLL.HashingHelper.GenerateSalt();
            var u1PW = BLL.HashingHelper.HashPassword(clearTextPW, salt);
            var u1 = new User { Id = 1, Email = "1@1.1", Firstname = "user1", Lastname = "1", Password = u1PW, Salt = salt };

            var ctrl = new BLL.UserCtrl();
            // Act
            bool isValid = ctrl.ValidatePassword(u1, "wrongPassword");

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
