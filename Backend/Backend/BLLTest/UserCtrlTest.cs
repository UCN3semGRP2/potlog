﻿using BLL;
using Model;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System.Linq;

namespace BLLTest
{
    [TestClass]
    public class UserCtrlTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            UserCtrl uCtrl = new UserCtrl();
            User user = new User
            {
                Firstname = "Niklas",
                Lastname = "Jørgensen",
                Email = "n@n.dk" + Guid.NewGuid(), // To avoid creating the same user when rerunning the test
                Password = "1234"
            };

            uCtrl.CreateUser(user.Firstname, user.Lastname, user.Email, user.Password);

            User foundUser = null;
            using (DALContext db = new DALContext())
            {
                foundUser = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            }

            Assert.IsNotNull(foundUser);
            Assert.AreEqual(user.Firstname, foundUser.Firstname);
        }

        private User CreateTestUser(string clearTextPW)
        {
            var salt = BLL.HashingHelper.GenerateSalt();
            var u1PW = BLL.HashingHelper.HashPassword(clearTextPW, salt);
            var u1 = new User { Id = 1, Email = "1@1.1", Firstname = "user1", Lastname = "1", Password = u1PW, Salt = salt };
            return u1;
        }

        [TestMethod]
        public void TestValidatePassword_withValidPw()
        {
            // Arrange
            var clearTextPW = "hunter1";
            var u1 = CreateTestUser(clearTextPW);
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
            var u1 = CreateTestUser(clearTextPW);
            var ctrl = new BLL.UserCtrl();
            // Act
            bool isValid = ctrl.ValidatePassword(u1, "wrongPassword");

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void TestUserIsNotValidated()
        {
            // Arrange
            var ctrl = new BLL.UserCtrl();
            var u1 = CreateTestUser("hunter1");
            // u1 not validated

            // ACT
            bool isValidated = ctrl.IsValidated(u1);

            // Assert
            Assert.IsFalse(isValidated);
        }

        [TestMethod]
        public void TestUserIsValidated()
        {
            // Arrange
            var ctrl = new BLL.UserCtrl();
            var u1 = CreateTestUser("hunter1");
            u1.LogInSession = new Session();

            // ACT
            bool isValidated = ctrl.IsValidated(u1);

            // Assert
            Assert.IsTrue(isValidated);
        }

        [TestMethod]
        public void TestUserIsNotValidatedExpired()
        {
            // Arrange
            var ctrl = new BLL.UserCtrl();
            var u1 = CreateTestUser("hunter1");
            u1.LogInSession = new Session();
            u1.LogInSession.ExpireDate = Convert.ToDateTime("2010-12-25 16:58:00"); //new TimeSpan(5 * 60, 0, 0);  //new DateTime(1850, 1, 1); //DateTime.Parse("1-1-1850"); // expired in the past

            // ACT
            bool isValidated = ctrl.IsValidated(u1);

            // Assert
            Assert.IsFalse(isValidated);
       }
    }
}
