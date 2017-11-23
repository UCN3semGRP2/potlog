using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UnitOfWork
    {
        private SessionCtrl sesCtrl = new SessionCtrl();
        private UserCtrl uCtrl = new UserCtrl();
        private EventCtrl eCtrl = new EventCtrl();

        //public void SignUpForEvent(User u, Event e)
        //{
        //    using (var ctx = new DALContext())
        //    {
        //        var evnt = new EventCtrl().RegisterToEvent(ctx, e, u);
        //        ctx.SaveChanges();
        //    }
        //}

        public Registration SignUpForEvent(string userEmail, int eventId)
        {
            using (var ctx = new DALContext())
            {
                Event e = eCtrl.FindById(ctx, eventId);
                User u = uCtrl.FindByEmail(userEmail);
                var reg = new EventCtrl().RegisterToEvent(ctx, e, u);
                ctx.SaveChanges();
                return reg;
            }
        }

        public bool IsRegisteredToEvent(User u, Event e)
        {
            using (var ctx = new DALContext())
            {
                if (!uCtrl.IsValidated(u))
                {
                    return false;
                }

                return uCtrl.IsRegisteredToEvent(u, e);
            }
        }

        //public void SignUpForEvent(Guid sessionId)
        //{
        //    using (var ctx = new DALContext())
        //    {

        //        //Session ses = sesCtrl.FindSessionByGuid(ctx, sessionId);
        //        User u = uCtrl.FindBySessionId(sessionId);
        //    }
        //}

        public Event CreateEvent(string title, string description, int numOfParticipants, double priceFrom, double priceTo, string location, DateTime datetime, bool isPublic)
        {
            using (var ctx = new DALContext())
            {
                Event e = eCtrl.CreateEvent(ctx, title, description, numOfParticipants, priceFrom, priceTo, location, datetime, isPublic);

                ctx.SaveChanges();
                return e;
            }
        }

        public Event FindEventById(int id)
        {
            using (var ctx = new DALContext())
            {
                var e = eCtrl.FindById(ctx, id);
                return e;
            }
        }
    }
}
