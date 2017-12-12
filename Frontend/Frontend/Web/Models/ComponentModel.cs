using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class ComponentModel
    {
        [HiddenInput(DisplayValue = false)]
        public int EventId { get; set; }
        [Display(Name = "Hovedkategori:")]
        public int LevelOneId { get; set; }
        [Display(Name = "Niveau 2:")]
        public int LevelTwoId { get; set; }
        [Display(Name = "Niveau 3:")]
        public int LevelThreeId { get; set; }
        [Display(Name = "Hovedkategori:")]
        public List<SelectListItem> LevelOneList { get; set; }
        [Display(Name = "Niveau 2:")]
        public List<SelectListItem> LevelTwoList { get; set; }
        [Display(Name = "Niveau 3:")]
        public List<SelectListItem> LevelThreeList { get; set; }

        public ComponentModel()
        {
            this.LevelOneList = new List<SelectListItem>();
            this.LevelTwoList = new List<SelectListItem>();
            this.LevelThreeList = new List<SelectListItem>(); 
        }
    }
}