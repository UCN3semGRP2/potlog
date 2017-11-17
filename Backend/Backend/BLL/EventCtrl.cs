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
            return db.Create(e);
        }

        public Registration RegisterToEvent(Event newEvent, User user)
        {
            throw new NotImplementedException();
        }
    }
}
