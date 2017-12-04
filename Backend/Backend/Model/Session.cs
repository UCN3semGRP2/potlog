using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract(IsReference = true)]
    public class Session
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Guid GUID { get; set; }

        [DataMember]
        public DateTime ExpireDate { get; set; }

        public Session()
        {
            GUID = Guid.NewGuid();
            ExpireDate = DateTime.Now.AddMinutes(4 * 60);
        }
    }
}
