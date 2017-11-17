using BLL;
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
            UserCtrl uCtrl = new UserCtrl();
            uCtrl.CreateUser("Jesper", "Jørgensen", "e@w.dk", "1234");
        }
    }
}
