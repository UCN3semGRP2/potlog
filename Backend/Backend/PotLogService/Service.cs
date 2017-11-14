using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL;
using Model;

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

        public User LogIn(string email, string clearTextPw)
        {
            return uCtrl.LogIn(email, clearTextPw);
        }
    }
}
