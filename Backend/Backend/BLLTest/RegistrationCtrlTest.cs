using BLL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTest
{
    [TestClass]
    public class RegistrationCtrlTest
    {
        [TestMethod]
        public void CreateRegistrationTest()
        {
            // Arrange
            UserCtrl uCtrl = new UserCtrl();
            var user = uCtrl.CreateUser("Jesper", "Jørgensen", "e@w.dk" + Guid.NewGuid(), "1234");
            EventCtrl eCtrl = new EventCtrl();
            var eve = eCtrl.CreateEvent("Hej", "nej", 5, 5.5, 6.5, "42", DateTime.Now, false, user);
            RegistrationCtrl rCtrl = new RegistrationCtrl();

            // Act
            var reg = rCtrl.CreateRegistration(user, eve);

            //Assert
            bool userHasReg = user.Registrations.Contains(reg);
            bool eventHasReg = eve.Registrations.Contains(reg);
            Assert.IsTrue(userHasReg);
            Assert.IsTrue(eventHasReg);
        }
    }
}
