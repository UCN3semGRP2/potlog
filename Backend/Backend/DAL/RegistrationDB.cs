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

        public Registration Create(Registration entity)
        {
            Registration reg = null;
            using (DALContext ctx = new DALContext())
            {
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        reg = ctx.Registrations.Add(entity);
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                        return reg;
                    }
                    catch (Exception)
                    {
                        ctxTransaction.Rollback();
                        return reg;
                    }
                }
            }
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
