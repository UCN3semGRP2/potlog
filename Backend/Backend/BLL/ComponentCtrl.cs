using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class ComponentCtrl
    {
        public ICategoryDB catDB = new CategoryDB();
        public IItemDB iDB = new ItemDB();

        public Category CreateCategory(string title, string description, Component parent)
        {
            var c = new Category
            {
                Title = title,
                Description = description,
                Components = new List<Component>(),
                Parent = parent
                
            };
            return catDB.Create(c);
        }

        public Category FindCategoryById(int id)
        {
            return catDB.FindByID(id);
        }

        public Item CreateItem(string itemTitle, string itemDescription, int itemAmount, Component parent)
        {
            var item = new Item
            {
                Title = itemTitle,
                Description = itemDescription,
                Amount = itemAmount,
                Parent = parent
            };

            return iDB.Create(item);
        }

        public IEnumerable<Component> FindComponentByParentId(int id)
        {
            return catDB.FindComponentByParentId(id);
        }
    }
}
