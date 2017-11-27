using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public class EventDB : IEventDB
    {
        public Event Create(Event entity)
        {
            Event e = null;
            using (var ctx = new DALContext())
            {
                if (entity.Admin != null) ctx.Users.Attach(entity.Admin);

                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        e = ctx.Events.Add(entity);
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                        return e;
                    }
                    catch (Exception err)
                    {

                        ctxTransaction.Rollback();
                        throw err;

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
                return ctx.Events
                    .Include(x => x.Registrations).Include(x => x.Components)
                    .Where(x => x.Id == id)
                    .First(); //.Find(id);
                //return ctx.Events.Where(x => x.Id == id).Intersect()
            }
        }

        public void Update(Event entity)
        {
            using (var ctx = new DALContext())
            {
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                    try
                    {
                        ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                    }
                    catch (Exception)
                    {

                        ctxTransaction.Rollback();
                    }
            }
        }
    }
}
