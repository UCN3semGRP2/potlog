using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class UserDB
    {
        public void Create(User user)
        {
            using (DALContext db = new DALContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public User FindByEmail(string email)
        {
            using (DALContext db = new DALContext())
            {
                return db.Users.FirstOrDefault(x => x.Email == email);
            }
        }
    }
}
