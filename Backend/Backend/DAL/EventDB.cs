using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EventDB
    {
        private DALContext ctx = new DALContext();

        public void CreateEvent(Event e)
        {
            ctx.Events.Add(e);

            ctx.SaveChanges();
        }
    }
}
