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
        private UserCtrl uCtrl = new UserCtrl();

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic)
        {
            return UoW.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic);
        }

        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            uCtrl.CreateUser(firstName, lastName, email, password);
        }

        public User LogIn(string email, string clearTextPw)
        {
            return uCtrl.LogIn(email, clearTextPw);
        }

        public Registration SignUpForEvent(string userEmail, int eventId)
        {
            return UoW.SignUpForEvent(userEmail, eventId);
        }

        public Event FindEventById(int id)
        {
            return UoW.FindEventById(id);
        }

        public bool IsRegisteredToEvent(User u, Event e)
        {
            return UoW.IsRegisteredToEvent(u, e);
        }
    }
}
