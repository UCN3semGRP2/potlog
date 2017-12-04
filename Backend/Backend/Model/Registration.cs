using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract(IsReference = true)]
    public class Registration
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime DateOfCreation { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public Event Event { get; set; }
    }
}