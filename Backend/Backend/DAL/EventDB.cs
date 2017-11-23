using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EventDB : IEventDB
    {
        public Event Create(Event entity)
        {
            Event e = null;
            using (var ctx = new DALContext())
            {
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        e = ctx.Events.Add(entity);
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                        return e;
                    }
                    catch (Exception)
                    {

                        ctxTransaction.Rollback();
                        return e;

                    }
                }
            }
        }

        public void Delete(Event entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> FindAll()
        {
            throw new NotImplementedException();
        }

        public Event FindByID(int id)
        {
            using (DALContext ctx = new DALContext())
            {
                return ctx.Events.Find(id);
            }
        }

        public void Update(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
