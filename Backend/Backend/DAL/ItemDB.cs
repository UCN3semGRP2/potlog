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
            using (var ctx = new DALContext())
            {
                ctx.Database.Log = (s) => {
                    Console.WriteLine(entity);
                    System.Diagnostics.Debug.WriteLine(s);
                };//Console.Write;
                using (var ctxTransaction = ctx.Database.BeginTransaction())
                {
                    if (entity.Parent != null)
                    {
                        ctx.Components.Attach(entity.Parent);
                    }

                    if (entity.Event != null)
                    {
                        ctx.Events.Attach(entity.Event);
                    }

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
