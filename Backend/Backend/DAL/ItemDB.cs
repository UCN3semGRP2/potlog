using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class ItemDB : IItemDB
    {
        public Item Create(Item entity)
        {
            using (DALContext ctx = new DALContext())
            {
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Item item = (Item)ctx.Components.Add(entity);
                        ctx.SaveChanges();
                        ctxTransaction.Commit();
                        return item;
                    }
                    catch (Exception err)
                    {
                        ctxTransaction.Rollback();
                        throw err;
                    }
                }
            }
        }

        public void Delete(Item entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> FindAll()
        {
            throw new NotImplementedException();
        }

        public Item FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
