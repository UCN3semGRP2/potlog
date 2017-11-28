using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumOfParticipants { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public string Location { get; set; }
        public DateTime Datetime { get; set; }
        public bool IsPublic { get; set; }
        public List<Registration> Registrations { get; set; }
        public User Admin { get; set; }
        public List<Component> Components { get; set; }
    }
}
