﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICategoryDB : ICRUD<Category>
    {
        List<Component> FindComponentByParentId(int id);
        Item FindItemByID(int itemId);
    }
}
