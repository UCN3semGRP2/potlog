using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class ComponentModel
    {
        public int LevelOneId { get; set; }
        public int LevelTwoId { get; set; }
        public int LevelThreeId { get; set; }
        public List<SelectListItem> LevelOneList { get; set; }
        public List<SelectListItem> LevelTwoList { get; set; }
        public List<SelectListItem> LevelThreeList { get; set; }

        public ComponentModel()
        {
            this.LevelOneList = new List<SelectListItem>();
            this.LevelTwoList = new List<SelectListItem>();
            this.LevelThreeList = new List<SelectListItem>(); 
        }
    }
}