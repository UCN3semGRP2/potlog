using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class RegistrationCtrl
    {
        RegistrationDB rDB = new RegistrationDB();
        public Registration CreateRegistration(User user, Event eve)
        {
            var reg = new Registration
            {
                DateOfCreation = DateTime.Now,
                Event = eve,
                User = user
            };
            var finalReg = rDB.Create(reg);
            rDB.Commit();
            return finalReg;
        }
    }
}
