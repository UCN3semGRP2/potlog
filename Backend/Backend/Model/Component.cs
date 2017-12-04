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
    [DataContract]
    public abstract class Component
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Event Event { get; set; }
        public int? EventId { get; set; }
        public Component Parent { get; set; }
        public override string ToString()
        {
            return this.Title;
        }
    }

}
