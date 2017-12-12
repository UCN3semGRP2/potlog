using System;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Index("IX_UniqueUserReg", 1, IsUnique = true)]
        public int? UserId { get; set; }

        [DataMember]
        public Event Event { get; set; }
        [Index("IX_UniqueUserReg", 2, IsUnique = true)]
        public int? EventId { get; set; }
    }
}