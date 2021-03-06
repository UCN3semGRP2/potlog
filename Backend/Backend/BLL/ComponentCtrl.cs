﻿using System;
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

        public Item CreateItem(string itemTitle, string itemDescription, int itemAmount, Component parent, Event e)
        {
            var item = new Item
            {
                Title = itemTitle,
                Description = itemDescription,
                Amount = itemAmount,
                Parent = parent,
                EventId = e.Id
            };

            return iDB.Create(item);
        }

        public List<Component> FindComponentByParentId(int id)
        {
            return catDB.FindComponentByParentId(id);
        }

        public void Update(Category c)
        {
            catDB.Update(c);
        }

        public Item FindItemById(int itemId)
        {
            return catDB.FindItemByID(itemId);
	}
	
	public bool HasParentCategory(Category c)
        {
            return c.Parent != null && c.Parent is Category;
        }

        public void AttachCategoryToItsParent(Category c)
        {
            var p = (Category)c.Parent;
            if (p.Components == null)
            {
                p.Components = new List<Component>();
            }
            p.Components.Add(c);
            c.Event = p.Event;
            c.EventId = p.EventId;
            this.Update(c);
            this.Update(p);
        }

        internal bool ItemHasARegisteredUser(Item item)
        {
            item = this.FindItemById(item.Id);
            return item.Registration != null;
        }
    }
}
