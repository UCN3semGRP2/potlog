using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Runtime.Serialization;

namespace BLL
{
    public class EventCtrl
    {
        private RegistrationCtrl rCtrl = new RegistrationCtrl();
        private UserCtrl uCtrl = new UserCtrl();
        private IEventDB eDB = new EventDB();

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic, User admin)
        {
            if (priceFrom > priceTo)
            {
                throw new ArgumentException("PriceTo must be larger than priceFrom");
            }
            if (datetime < DateTime.Now)
            {
                throw new DateInPastException("Time and date set is in the past");
            }
            var e = new Event
            {
                Title = title,
                Description = description,
                NumOfParticipants = numOfParticipants,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                Location = location,
                Datetime = datetime,
                IsPublic = isPublic,
                Admin = admin
            };
            var finalEvent = eDB.Create(e);
            return finalEvent;
        }

        public Event FindById(int eventId)
        {
            return eDB.FindByID(eventId);
        }

        /// <summary>
        /// Registor to event registors the given user to the given event. 
        /// </summary>
        /// <param name="evnt">Note: Event is not valid after the method has been called</param>
        /// <param name="user">Note: User is not valid after the method has been called</param>
        public void RegisterToEvent(Event evnt, User user)
        {
            rCtrl.CreateRegistration(user, evnt);
        }

        public void SignUpForEvent(string userEmail, int eventId)
        {
                Event e = FindById(eventId);
                User u = uCtrl.FindByEmail(userEmail);
                RegisterToEvent(e, u);
        }

        public void AddCategory(Event e, Category c)
        {
            if (e.Components == null)
            {
                e.Components = new List<Component>();
            }
            e.Components.Add(c);
            c.Event = e;
            c.EventId = e.Id;
            eDB.Update(e);
        }
    }
}
