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
        DALContext context = new DALContext();
        public Registration Create(Registration entity)
        {
            var reg =context.Registrations.Add(entity);
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

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
