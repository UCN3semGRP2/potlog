using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Transactions;
using Model;

namespace DAL
{
    public class UserDB : IUserDB
    {
        //private DALContext ctx = new DALContext();

        public UserDB()
        {
           
        }

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
                    catch (Exception)
                    {

                        ctxTransaction.Rollback();
                        return u;
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
            return ctx.Users.FirstOrDefault(x => x.Email == email);
        }

        public User FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

    }
}
