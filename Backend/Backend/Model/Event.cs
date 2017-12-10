using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract(IsReference = true)]
    public class Event
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int NumOfParticipants { get; set; }

        [DataMember]
        public double PriceFrom { get; set; }

        [DataMember]
        public double PriceTo { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public DateTime Datetime { get; set; }

        [DataMember]
        public bool IsPublic { get; set; }

        [DataMember]
        public List<Registration> Registrations { get; set; }

        [DataMember]
        [Required]
        public User Admin { get; set; }

        [DataMember]
        public List<Component> Components { get; set; }

        [DataMember]
        public string InviteString { get; set; }
    }
}
