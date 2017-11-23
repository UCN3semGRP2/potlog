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
    public class EventCtrlTest
    {
        [TestMethod]
        public void TestCreateEvent()
        {
            var ctrl = new EventCtrl();

            Event e = new Event
            {
                Title = "test title" + Guid.NewGuid(),
                Description = "This is a long description\ncontaining newlines",
                NumOfParticipants = 5,
                PriceFrom = 100.0,
                PriceTo = 200.0,
                Location = "Sofiendalsvej 60",
                Datetime = DateTime.Now,
                IsPublic = true
            };


            using (var ctx = new DALContext())
            {
                // Act
                Event output = ctrl.CreateEvent(ctx, e.Title, e.Description, e.NumOfParticipants, e.PriceFrom, e.PriceTo, e.Location, e.Datetime, e.IsPublic, null);

                // Assert
                Assert.AreEqual(e.Title, output.Title);

            }

        }
        [TestMethod]
        public void TestRegisterToEvent()
        {
            using (var ctx = new DALContext())
            {
                // Arrange
                var ctrl = new EventCtrl();
                var uCtrl = new UserCtrl();

                Event e = new Event
                {
                    Title = "test title" + Guid.NewGuid(),
                    Description = "This is a long description\ncontaining newlines",
                    NumOfParticipants = 5,
                    PriceFrom = 100.0,
                    PriceTo = 200.0,
                    Location = "Sofiendalsvej 60",
                    Datetime = DateTime.Now,
                    IsPublic = true
                };
                var user = uCtrl.CreateUser(ctx, "piss", "pee", "tis@urin.dk" + Guid.NewGuid(), "pissurinkage");
                Event newEvent = ctrl.CreateEvent(ctx, e.Title, e.Description, e.NumOfParticipants, e.PriceFrom, e.PriceTo, e.Location, e.Datetime, e.IsPublic, user);


                // Act
                Registration reg = ctrl.RegisterToEvent(ctx, newEvent, user);

                // Assert
                Assert.AreEqual(reg.User, user);
                Assert.AreEqual(reg.Event, newEvent);
                Assert.IsTrue(user.Registrations.Contains(reg));
                Assert.IsTrue(newEvent.Registrations.Contains(reg));
            }
        } 
    }
}
