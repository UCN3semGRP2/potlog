using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PotLogServiceTests
{
    [TestClass]
    public class LogInTests
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        [TestMethod]
        public void TestLogInHappyDay()
        {
            var firstName = "firstname";
            var lastName = "lastname";
            var email = "email@address.dk" + Guid.NewGuid();
            var password = "hunter1";

            // Act
            service.CreateUser(firstName, lastName, email, password);
            var user = service.LogIn(email, password);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(firstName, user.Firstname);
            Assert.AreEqual(lastName, user.Lastname);
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        public void TestLogInInvalidPassword()
        {
            var firstName = "firstname";
            var lastName = "lastname";
            var email = "email@address.dk" + Guid.NewGuid();
            var password = "hunter1";

            // Act
            service.CreateUser(firstName, lastName, email, password);
            var user = service.LogIn(email, "Wrong password");

            // Assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public void TestLogInWrongEmail()
        {
            var firstName = "firstname";
            var lastName = "lastname";
            var email = "email@address.dk" + Guid.NewGuid();
            var password = "hunter1";

            // Act
            service.CreateUser(firstName, lastName, email, password);
            var user = service.LogIn("Not@aValid.email", password);

            // Assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public void TestLogInWrongEmailAndPassword()
        {
            var firstName = "firstname";
            var lastName = "lastname";
            var email = "email@address.dk" + Guid.NewGuid();
            var password = "hunter1";

            // Act
            service.CreateUser(firstName, lastName, email, password);
            var user = service.LogIn("Not@aValid.email", "Wrong password");

            // Assert
            Assert.IsNull(user);
        }
    }
}
