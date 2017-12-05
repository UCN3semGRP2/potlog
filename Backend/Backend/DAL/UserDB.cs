using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace DAL
{
    public class UserDB : IUserDB
    {
        public User Create(User user)
        {
            User u = null;
            using (var ctx = new DALContext())
            {
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        u = ctx.Users.Add(user);
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                        return u;
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ctxTransaction.Rollback();
                        return null;
                    }
                    catch (Exception err)
                    {

                        ctxTransaction.Rollback();
                        throw err;
                    }

                }
            }
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public User FindByEmail(string email)
        {
            using (var ctx = new DALContext())
            {
                return ctx.Users
                    .Include(x => x.Registrations.Select(y => y.Event))
                    .Where(x => x.Email == email)
                    .FirstOrDefault();
                //return ctx.Users.FirstOrDefault(x => x.Email == email);
            }
        }

        public User FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsRegisteredToEvent(User user, Event evnt)
        {
            using (var ctx = new DALContext())
            {
                var userRegs = ctx.Users.Include(x => x.Registrations.Select(reg => reg.Event)).Single(x => x.Id == user.Id).Registrations;
                var eventRegs = ctx.Events.Include(x => x.Registrations.Select(reg => reg.User)).Single(x => x.Id == evnt.Id).Registrations;
                var userEventReg = userRegs.Intersect(eventRegs).SingleOrDefault(reg => reg.Event.Id == evnt.Id && reg.User.Id == user.Id);
                return userEventReg != null;
               //ctx.Users.Where(u => u.Id == user.Id)
               //     .Include(u => u.Registrations
               //         .Select(reg => reg.Event))
               //     .Where(u => u.Registrations)
                    
            }
        }

        public void Update(User entity)
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
