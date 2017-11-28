using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class CategoryDB : ICategoryDB
    {
        public Category Create(Category entity)
        {
            using (var ctx = new DALContext())
            {
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Category cat = (Category)ctx.Components.Add(entity);
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                        return cat;
                    }
                    catch (Exception err)
                    {
                        ctxTransaction.Rollback();
                        throw err;
                    }
                }
            }
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> FindAll()
        {
            throw new NotImplementedException();
        }

        public Category FindByID(int id)
        {
            using (var ctx = new DALContext())
            {
                return (Category)ctx.Components
                    .Where(x => x is Category)
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
            }
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
