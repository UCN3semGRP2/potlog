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

        public Event CreateEvent(DALContext ctx, string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic)
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
            var finalEvent = new EventDB(ctx).Create(e);
            return finalEvent;
        }

        public Registration RegisterToEvent(DALContext ctx, Event e, User user)
        {
            Registration reg = rCtrl.CreateRegistration(ctx, user, e);
            e.Registrations.Add(reg);
            uCtrl.AddRegistration(ctx, user, reg);
            return reg;
        }
    }
}
