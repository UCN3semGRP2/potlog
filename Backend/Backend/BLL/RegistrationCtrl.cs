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
        private IRegistrationDB rDB = new RegistrationDB();

        public Registration CreateRegistration(User user, Event evnt)
        {

            var reg = new Registration
            {
                DateOfCreation = DateTime.Now,
                Event = evnt,
                User = user
            };

            try
            {
                var finalReg = rDB.Create(reg);
                return finalReg;
            }
            catch (DuplicateRegistrationException)
            {
                throw new ArgumentException("The user is already registered to the event");
            }
        }
    }
}
