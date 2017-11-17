using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CreateEventViewModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int NumOfParticipants { get; set; }
        public bool IsPublic { get; set; }
        public string Description { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
    }
}