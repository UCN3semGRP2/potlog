using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class EventCtrl
    {
        private RegistrationCtrl rCtrl = new RegistrationCtrl();
        private UserCtrl uCtrl = new UserCtrl();
        private EventDB eDB = new EventDB();

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic)
        {
            var e = new Event
            {
                Title = title,
                Description = description,
                NumOfParticipants = numOfParticipants,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                Location = location,
                Datetime = datetime,
                IsPublic = isPublic
            };
            var finalEvent = eDB.Create(e);
            return finalEvent;
        }

        public Event FindById(int eventId)
        {
            return eDB.FindByID(eventId);
        }

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

        public Event FindEventById(int id)
        {
            return eDB.FindByID(id);
        }
    }
}
