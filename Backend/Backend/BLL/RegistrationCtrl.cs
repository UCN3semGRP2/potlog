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
        public Registration CreateRegistration(DALContext ctx, User user, Event eve)
        {
            var reg = new Registration
            {
                DateOfCreation = DateTime.Now,
                Event = eve,
                User = user
            };
            var finalReg = new RegistrationDB(ctx).Create(reg);
            return finalReg;
        }
    }
}
