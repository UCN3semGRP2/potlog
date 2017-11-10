using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL;

namespace PotLogService
{
    public class Service : IService
    {
        UserCtrl uCtrl = new UserCtrl();
        public void CreateUser(string Firstname, string Lastname, string Email, string Password)
        {
            uCtrl.CreateUser(Firstname, Lastname, Email, Password);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}
