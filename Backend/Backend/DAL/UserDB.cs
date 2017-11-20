using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace DAL
{
    public class UserDB : ICRUD<User>
    {
        private DALContext ctx;

        public UserDB(DALContext ctx)
        {
            this.ctx = ctx;
        }

        public User Create(User user)
        {
                var u = ctx.Users.Add(user);
                return u;
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
