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
            var user = uCtrl.CreateUser("Jesper", "Jørgensen", "e@w.dk" + Guid.NewGuid(), "123456");
            EventCtrl eCtrl = new EventCtrl();
            var eve = eCtrl.CreateEvent("Hej", "nej", 5, 5.5, 6.5, "42", DateTime.Now.AddHours(5), false, user);
            RegistrationCtrl rCtrl = new RegistrationCtrl();

            // Act
            var reg = rCtrl.CreateRegistration(user, eve);

            //Assert
            user = uCtrl.UpdateUserInfo(user);
            eve = eCtrl.FindById(eve.Id);

            bool userHasReg = user.Registrations.Select(x => x.Id).Contains(reg.Id);
            bool eventHasReg = eve.Registrations.Select(x => x.Id).Contains(reg.Id);
            Assert.IsTrue(userHasReg);
            Assert.IsTrue(eventHasReg);
        }
    }
}
