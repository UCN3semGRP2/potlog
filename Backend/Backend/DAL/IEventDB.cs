using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IEventDB : ICRUD<Event>
    {
        Event FindByID(int id);
        IEnumerable<Event> FindAll();
        Event Create(Event entity);
        void Update(Event entity);
        void Delete(Event entity);
    }
}
