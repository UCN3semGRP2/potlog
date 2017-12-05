﻿using System;
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
        private UserCtrl uCtrl = new UserCtrl();
        private EventCtrl eCtrl = new EventCtrl();
        private RegistrationCtrl rCtrl = new RegistrationCtrl();
        private ComponentCtrl cCtrl = new ComponentCtrl();

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic, User admin)
        {
            try
            {
                return eCtrl.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);
            }
            catch (ArgumentException ae)
            {
                throw new FaultException(ae.Message);
            }
            catch (DateInPastException dipe)
            {
                throw new FaultException(dipe.Message);
            }
        }

        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            try
            {
                uCtrl.CreateUser(firstName, lastName, email, password);
            }
            catch (DublicateUserException)
            {
                throw new FaultException("Brugeren eksisterer allerede");
            }
        }

        public User LogIn(string email, string clearTextPw)
        {
            User u = uCtrl.LogIn(email, clearTextPw);
            return u;
        }

        public void SignUpForEvent(string userEmail, int eventId)
        {
            eCtrl.SignUpForEvent(userEmail, eventId);
        }

        public Event FindEventById(int id)
        {
            return eCtrl.FindById(id);
        }

        public bool IsRegisteredToEvent(User u, Event e)
        {

            return uCtrl.IsRegisteredToEvent(u, e);
        }

        public void AddCategoryToEvent(int eventId, string categoryTitle, string categoryDescription)
        {
            //TODO refactor to ectrl
            Category c = cCtrl.CreateCategory(categoryTitle, categoryDescription);
            Event e = eCtrl.FindById(eventId);
            eCtrl.AddCategory(e, c);
        }

        public Category FindCategoryById(int id)
        {
            return cCtrl.FindCategoryById(id);
        }
    }
}
