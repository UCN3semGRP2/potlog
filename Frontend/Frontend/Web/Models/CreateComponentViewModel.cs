using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class CreateComponentViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int EventId { get; set; }
        [Required]
        [Display(Name = "Øvre kategori")]
        public List<SelectListItem> Categories { get; set; }
        public string SelectedCategory { get; set; }
        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }
    }
}