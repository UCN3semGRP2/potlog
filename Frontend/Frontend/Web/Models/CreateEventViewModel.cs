using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CreateEventViewModel
    {
        [Display(Name = "Event Navn")]
        [Required(ErrorMessage = "Event Navn skal udfyldes.")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Sted")]
        [Required(ErrorMessage = "Sted skal udfyldes.")]
        [DataType(DataType.Text)]
        public string Location { get; set; }

        [Display(Name = "Dato")]
        [Required(ErrorMessage = "Dato skal udfyldes.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Tid")]
        [Required(ErrorMessage = "Tid skal udfyldes.")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:HH\\:mm}")] // 24 hour time picker
        public TimeSpan Time { get; set; }

        [Display(Name = "Max Antal Deltagere")]
        [Required(ErrorMessage = "Max Antal Deltagere udfyldes.")]
        public int NumOfParticipants { get; set; }

        [Display(Name = "Offentlig Event")]
        [Required(ErrorMessage = "Event skal enten være offentlig eller privat.")]
        public bool IsPublic { get; set; }

        [Display(Name = "Beskrivelse")]
        [Required(ErrorMessage = "Beskrivelse skal udfyldes.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Pris Fra")]
        [Required(ErrorMessage = "Pris Fra skal udfyldes.")]
        [DataType(DataType.Currency)]
        public double PriceFrom { get; set; }

        [Display(Name = "Pris Til")]
        [Required(ErrorMessage = "Pris Til skal udfyldes.")]
        [DataType(DataType.Currency)]
        public double PriceTo { get; set; }
    }
}