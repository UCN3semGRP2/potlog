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
        private UserCtrl uCtrl = new UserCtrl();
        private EventCtrl eCtrl = new EventCtrl();
        private RegistrationCtrl rCtrl = new RegistrationCtrl();
        private ComponentCtrl cCtrl = new ComponentCtrl();

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic, User admin)
        {
            try
            {
                Event e = eCtrl.CreateEvent(title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic, admin);
                Registration r = rCtrl.CreateRegistration(e.Admin, e);
                uCtrl.AddRegistration(e.Admin, r);
                return e;

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
            catch (ArgumentException argE)
            {
                throw new FaultException(argE.Message);
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

        public void AddCategoryToEvent(int eventId, string categoryTitle, string categoryDescription, Component parent)
        {
            if (categoryTitle == "")
            {
                throw new ArgumentException("Der skal indtastes en titel");
            }
            if (categoryDescription == "")
            {
                throw new ArgumentException("Der skal indtastes en beskrivelse");
            }

            // TODO could refactor to ectrl
            Category c = cCtrl.CreateCategory(categoryTitle, categoryDescription, parent);
            Event e = eCtrl.FindById(eventId);
            eCtrl.AddCategory(e, c);
        }


        public Category FindCategoryById(int id)
        {
            return cCtrl.FindCategoryById(id);
        }

        public User UpdateUserInfo(User u)
        {
            return uCtrl.UpdateUserInfo(u);
        }

        public void AddItemToCategory(int eventId, int categoryId, int amount, string itemTitle, string itemDescription)
        {
            Category c = cCtrl.FindCategoryById(categoryId);
            Event e = eCtrl.FindById(eventId);
            Item item = cCtrl.CreateItem(itemTitle, itemDescription, amount, c);
            eCtrl.AddItem(e, c, item);
        }


        public List<Component> FindComponentByParentId(int id)
        {
            return cCtrl.FindComponentByParentId(id);
        }

        public string GetInviteString(Event evnt, User usr)
        {
            return eCtrl.GetInviteString(evnt, usr);
        }

        public Event AcceptInviteString(User usr, string inviteString)
        {
            return uCtrl.AcceptInviteString(usr, inviteString);
        }
    }
}
