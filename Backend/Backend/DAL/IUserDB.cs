using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IUserDB : ICRUD<User>
    {
        User FindByID(int id);
        IEnumerable<User> FindAll();
        User Create(User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
