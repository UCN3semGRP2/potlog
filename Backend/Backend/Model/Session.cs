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
        public Guid GUID { get; set; }
        public DateTime ExpireDate { get; set; }

        public Session()
        {
            GUID = Guid.NewGuid();
            ExpireDate = DateTime.Now.AddMinutes(4 * 60);
        }
    }
}
