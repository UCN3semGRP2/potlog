using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RegistrationDB : ICRUD<Registration>
    {
        private DALContext ctx;

        public RegistrationDB(DALContext ctx)
        {
            this.ctx = ctx;
        }

        public Registration Create(Registration entity)
        {
            var reg = ctx.Registrations.Add(entity);
            return reg;
        }

        public void Delete(Registration entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Registration> FindAll()
        {
            throw new NotImplementedException();
        }

        public Registration FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Registration entity)
        {
            throw new NotImplementedException();
        }

    }
}
