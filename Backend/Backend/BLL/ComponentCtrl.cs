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

        public Category CreateCategory(string title, string description)
        {
            var c = new Category
            {
                Title = title,
                Description = description,
                Components = new List<Component>()
            };
            return catDB.Create(c);
        }

        public Category FindCategoryById(int id)
        {
            return catDB.FindByID(id);
        }

        public Item CreateItem(string itemTitle, string itemDescription, int itemAmount)
        {
            var item = new Item
            {
                Title = itemTitle,
                Description = itemDescription,
                Amount = itemAmount
            };

            return iDB.Create(item);
        }
    }
}
