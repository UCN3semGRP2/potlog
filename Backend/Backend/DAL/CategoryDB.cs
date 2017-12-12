using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using System.Data.Entity.Migrations;

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
                        if (entity.Parent != null)
                        {
                            ctx.Components.Attach(entity.Parent);
                        }

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
                var query = ctx.Components.OfType<Category>()
                  .Where(x => x.Id == id)
                  .Include(x => x.Components);;
                var cat = query
                  .FirstOrDefault();
                cat.Components
                    .AddRange(ctx.Components.OfType<Item>()
                    .Where(item => item.Parent.Id == cat.Id));

                // note: Only resolves one level for the found Category
                foreach (var subcomp in cat.Components)
                {
                    if (subcomp is Category)
                    {
                        var sub = (Category)subcomp;
                        var items = ctx.Components.OfType<Item>()
                            .Where(x => x.Parent.Id == sub.Id).ToList();
                        sub.Components.AddRange(items);
                    }
                }
                  
                return cat;
                
                

            }
        }

        public List<Component> FindComponentByParentId(int id)
        {
            List<Component> components = null;
            using (var ctx = new DALContext())
            {
                components = ctx.Components.Where(c => c.Parent.Id == id).ToList();

            }

            return components;
        }

        public void Update(Category entity)
        {
            using (var ctx = new DALContext())
            {
                ctx.Components.AddOrUpdate(entity);
                ctx.SaveChanges();
            }
        }
    }
}
