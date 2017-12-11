using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class CreateComponentViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int EventId { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}