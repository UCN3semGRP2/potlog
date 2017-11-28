using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CreateComponentViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}