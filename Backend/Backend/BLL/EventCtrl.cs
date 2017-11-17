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
        private EventDB db = new EventDB();
        private RegistrationCtrl rCtrl = new RegistrationCtrl();
        private UserCtrl uCtrl = new UserCtrl();

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
            var finalEvent = db.Create(e);
            db.Commit();
            return finalEvent;
        }

        public Registration RegisterToEvent(Event e, User user)
        {
            Registration reg = rCtrl.CreateRegistration(user, e);
            e.Registrations.Add(reg);
            uCtrl.AddRegistration(user, reg);
            db.Commit();
            return reg;
        }
    }
}
