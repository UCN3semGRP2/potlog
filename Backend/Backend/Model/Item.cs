using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract(IsReference = true)]
    public class Item : Component
    {
        [DataMember]
        public int Amount { get; set; }
        public int ComponetId { get; set; }
        [DataMember]
        public Registration Registration { get; set; }
    }
}
