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
        EventCtrl eCtrl = new EventCtrl();

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic)
        {
            throw new NotImplementedException();
            //return eCtrl.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic);
        }

        public void CreateUser(string Firstname, string Lastname, string Email, string Password)
        {
            throw new NotImplementedException();
            //uCtrl.CreateUser(Firstname, Lastname, Email, Password);
        }

        public User LogIn(string email, string clearTextPw)
        {
            throw new NotImplementedException();
            //return uCtrl.LogIn(email, clearTextPw);
        }
    }
}
