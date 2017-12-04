using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;

namespace PotLogServiceTests
{
    [TestClass]
    public class CreateUserTests
    {
        private ServiceReference.IService service = new ServiceReference.ServiceClient();

        [TestMethod]
        public void TestCreateUserHappyDay()
        {
            var firstName = "firstname";
            var lastName = "lastname";
            var email = "email@address.dk" + Guid.NewGuid();
            var password = "hunter1";

            // Act
            service.CreateUser(firstName, lastName, email, password);

            // Assert
            var user = service.LogIn(email, password);
            Assert.IsNotNull(user);
            Assert.AreEqual(firstName, user.Firstname);
            Assert.AreEqual(lastName, user.Lastname);
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void TestCreateUserDuplicate()
        {
            var firstName = "firstname";
            var lastName = "lastname";
            var email = "email@address.dk" + Guid.NewGuid();
            var password = "hunter1";

            // Act
            service.CreateUser(firstName, lastName, email, password);
            
            // This should fail
            service.CreateUser(firstName, lastName, email, password);

            // Assert
            // Should never go here
            Assert.Fail();
        }
    }
}
