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
                throw new ArgumentException("Pris fra skal være mindre end pris til");
            }
            if (datetime < DateTime.Now)
            {
                throw new DateInPastException("Dato er sat i fortiden");
            }
            if (!(numOfParticipants > 0))
            {
                throw new ArgumentException("Der skal være mindst 1 deltager");
            }
            if (admin == null)
            {
                throw new ArgumentException("There must and admin for an event");
            }
            if (priceFrom < 0 || priceTo < 0)
            {
                throw new ArgumentException("Prisen må ikke være negativ");
            }
            if (title == "")
            {
                throw new ArgumentException("Der skal indtastes en titel");
            }
            if (description == "")
            {
                throw new ArgumentException("Der skal indtastes en beskrivelse");
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
                Admin = admin,
                InviteString = Guid.NewGuid().ToString()
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
            if (c.Parent != null && c.Parent is Category)
            {
                // Should be moved to Component/category/item ctrl
                var p = (Category)c.Parent;
                if (p.Components == null)
                {
                    p.Components = new List<Component>();
                }
                p.Components.Add(c);
                c.Event = p.Event;
                c.EventId = p.EventId;
                new ComponentCtrl().Update(c);
                new ComponentCtrl().Update(p);
                // ctx.Component.AddOrUpdate(p);
                // ctx.Component.AddOrUpdate(c);

            }
            else
            {
                e.Components.Add(c);
                c.Event = e;
                c.EventId = e.Id;
                eDB.Update(e);
            }

        }

        public Event FindByInviteString(string inviteString)
        {
            return eDB.FindFromInviteString(inviteString);
        }


        public void AddItem(Event evnt, Category category, Item item)
        {
            category.Components.Add(item);
            item.Event = category.Event;
            item.EventId = category.EventId;
            item.Parent = category;
            item.ComponetId = category.Id;
            eDB.Update(evnt);
        }

        public string GetInviteString(Event evnt, User usr)
        {
           // TODO verify that we are logged in and that usr is indeed admin for event
           if (evnt.Admin.Id != usr.Id)
           {
                throw new ArgumentException("the user is not admin for the event");
           }

            return evnt.InviteString;

        }
    }
}
