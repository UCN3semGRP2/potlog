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
        private UnitOfWork UoW = new UnitOfWork();

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic)
        {
            return UoW.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic);
        }

        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            UoW.CreateUser(firstName, lastName, email, password);
        }

        public User LogIn(string email, string clearTextPw)
        {
            return UoW.LogIn(email, clearTextPw);
        }
    }
}
