using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DetailsEventViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Event Navn")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Sted")]
        [DataType(DataType.Text)]
        public string Location { get; set; }

        [Display(Name = "Dato")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Tid")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:HH\\:mm}")]
        public TimeSpan Time { get; set; }

        [Display(Name = "Max Antal Deltagere")]
        public int NumOfParticipants { get; set; }

        [Display(Name = "Offentlig Event")]
        public bool IsPublic { get; set; }

        [Display(Name = "Beskrivelse")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Pris Fra")]
        [DataType(DataType.Currency)]
        public double PriceFrom { get; set; }

        [Display(Name = "Pris Til")]
        [DataType(DataType.Currency)]
        public double PriceTo { get; set; }
    }
}
