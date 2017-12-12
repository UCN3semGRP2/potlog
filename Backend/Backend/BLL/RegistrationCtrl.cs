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
            var finalReg = rDB.Create(reg);
            return finalReg;
        }

        public void CreateRegistrationForItem(User usr, Event evnt, Item item)
        {
            var reg = usr.Registrations.Where(x => x.Event.Id == evnt.Id).SingleOrDefault();
            if (reg == null)
            {
                throw new ArgumentNullException("The user is not registred to the event.");
            }
            if (reg.Items == null)
            {
                reg.Items = new List<Item>();
            }

            //item.Registration = reg;
            reg.Items.Add(item);
            rDB.Update(reg);
        }
    }
}
