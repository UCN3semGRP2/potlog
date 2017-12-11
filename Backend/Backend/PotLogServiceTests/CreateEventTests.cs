using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotLogServiceTests.ServiceReference;
using System.ServiceModel;

namespace PotLogServiceTests
{
    [TestClass]
    public class CreateEventTests
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        public User User { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var email = "t@t.t" + Guid.NewGuid();
            var pw = "hunter1";
            service.CreateUser("TestCreateEventUser", "Test", email, pw);
            this.User = service.LogIn(email, pw);
        }

        [TestMethod]
        public void TestCreateEventHappyDay()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = 5;
            double priceFrom = 5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);

            // Assert
            Assert.AreEqual(title, evnt.Title);
            Assert.AreEqual(description, evnt.Description);
            Assert.AreEqual(numOfParticipants, evnt.NumOfParticipants);
            Assert.AreEqual(priceFrom, evnt.PriceFrom);
            Assert.AreEqual(priceTo, evnt.PriceTo);
            Assert.AreEqual(location, evnt.Location);
            Assert.AreEqual(datetime, evnt.Datetime);
            Assert.AreEqual(isPublic, evnt.IsPublic);
            Assert.AreEqual(admin.Id, evnt.Admin.Id);
        }

        [TestMethod]
        public void TestCreateEventNullAdmin()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = 5;
            double priceFrom = 5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = null;

            try
            {
                var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);
                Assert.Fail();

            }
            catch (FaultException)
            {
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void TestCreateEventWithNoTitle()
        {
            string title = "";
            string description = "description";
            int numOfParticipants = 5;
            double priceFrom = 5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void TestCreateEventWithNoDescription()
        {
            string title = "title";
            string description = "";
            int numOfParticipants = 5;
            double priceFrom = 5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void TestCreateEventWithNegativeParticipants()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = -5;
            double priceFrom = 5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestCreateEventWithZeroParticipants()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = 0;
            double priceFrom = 5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            try
            {
                var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);
                Assert.Fail();

            }
            catch (FaultException)
            {
            }
        }

        [TestMethod]
        public void TestCreateEventWithNegativePriceFrom()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = 5;
            double priceFrom = -5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            try
            {
                var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);
                Assert.Fail();

            }
            catch (FaultException)
            {
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void TestCreateEventWithNegativePriceTo()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = 5;
            double priceFrom = -50;
            double priceTo = -10;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void TestCreateEventWithPriceToLesThanPriceFrom()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = 5;
            double priceFrom = 10;
            double priceTo = 5;
            string location = "her";
            DateTime datetime = DateTime.Now.AddDays(5);
            bool isPublic = false;
            User admin = User;

            var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void TestCreateEventWithDateInThePast()
        {
            string title = "title";
            string description = "description";
            int numOfParticipants = 5;
            double priceFrom = 5;
            double priceTo = 10;
            string location = "her";
            DateTime datetime = new DateTime(1990,3,3);
            bool isPublic = false;
            User admin = User;

            var evnt = service.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);

            // Assert
            Assert.Fail();
        }
    }
}
