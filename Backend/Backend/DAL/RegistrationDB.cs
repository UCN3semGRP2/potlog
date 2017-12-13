using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RegistrationDB : IRegistrationDB
    {

        public Registration Create(Registration entity)
        {
            using (DALContext ctx = new DALContext())
            {
                // User and Event objets was created by another ctx so we need to reattach them to this ctx
                entity.User = ctx.Users.Single(u => u.Id == entity.User.Id);
                entity.Event = ctx.Events.Single(e => e.Id == entity.Event.Id);

                //ctx.Events.Attach(entity.Event);
                //ctx.Entry(entity.Event).State = EntityState.Unchanged;
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.Registrations.AddOrUpdate(entity);
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                        return entity;
                    }
                    catch (Exception err)
                    {
                        ctxTransaction.Rollback();

                        
                        while (err.InnerException != null)
                        {
                            err = err.InnerException;
                            if (err.Message.Contains("IX_UniqueUserReg"))
                            {
                                throw new DuplicateRegistrationException();
                            }
                        }

                        throw err;
                    }
                }
            }
        }

        public void Delete(Registration entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Registration> FindAll()
        {
            throw new NotImplementedException();
        }

        public Registration FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Registration entity)
        {
            using (var ctx = new DALContext())
            {

                ctx.Registrations.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;

                ctx.Users.Attach(entity.User);
                ctx.Events.Attach(entity.Event);

                foreach (var item in entity.Items)
                {
                    ctx.Components.Attach(item);
                    ctx.Entry(item).State = EntityState.Modified;
                }

                ctx.SaveChanges();
            }
        }
    }

}

