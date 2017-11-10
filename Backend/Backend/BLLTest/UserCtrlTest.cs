using BLL;
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
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void CreateUserTest()
        {
            UserCtrl uCtrl = new UserCtrl();
            User user = new User
            {
                Firstname = "Niklas",
                Lastname = "Jørgensen",
                Email = "n@n.dk",
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
    }
}
