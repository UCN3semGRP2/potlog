﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EventDB : ICRUD<Event>
    {
        private DALContext ctx;

        public EventDB(DALContext ctx)
        {
            this.ctx = ctx;
        }

        public Event Create(Event entity)
        {
            var e = ctx.Events.Add(entity);
            return e;
        }


        public void Delete(Event entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> FindAll()
        {
            throw new NotImplementedException();
        }

        public Event FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
