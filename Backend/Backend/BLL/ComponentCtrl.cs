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

        public Category CreateCategory(string title, string description, int? parentId)
        {
            var c = new Category
            {
                Title = title,
                Description = description,
                Components = new List<Component>(),
                ParentId = parentId.Value
                
            };
            return catDB.Create(c);
        }

        public Category FindCategoryById(int id)
        {
            return catDB.FindByID(id);
        }

        public Item CreateItem(string itemTitle, string itemDescription, int itemAmount, int parentId)
        {
            var item = new Item
            {
                Title = itemTitle,
                Description = itemDescription,
                Amount = itemAmount,
                ParentId = parentId
            };

            return iDB.Create(item);
        }

        public List<Component> FindComponentByParentId(int id)
        {
            return catDB.FindComponentByParentId(id);
        }
    }
}
