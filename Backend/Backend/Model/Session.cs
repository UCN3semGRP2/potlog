using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Session
    {
        public int Id { get; set; }
        public Guid GUID { get;  }
        public DateTime ExpireDate { get;  }

        public Session()
        {
            GUID = Guid.NewGuid();
            ExpireDate = DateTime.Now.AddMinutes(4 * 60);
        }
    }
}
