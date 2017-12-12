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

        public Registration CreateRegistrationForItem(User usr, Event evnt, Item item)
        {
            var reg = new Registration
            {
                DateOfCreation = DateTime.Now,
                Event = evnt,
                User = usr,
                Items = new List<Item>()
            };

            reg.Items.Add(item);
            var finalReg = rDB.Create(reg);
            return finalReg;
        }
    }
}
