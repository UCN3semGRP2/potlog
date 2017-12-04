using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // Because Component is implemented by Category and Item we have to
    // add the knowntype and datacontract below for WCF to be able to
    // serialize/deserialize correctly
    [KnownType(typeof(Category))]
    [KnownType(typeof(Item))]
    [DataContract(IsReference = true)]
    public abstract class Component
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Event Event { get; set; }

        [DataMember]
        public int? EventId { get; set; }

        [DataMember]
        public Component Parent { get; set; }
    }

}
