using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Component
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Event Event { get; set; }
        public int? EventId { get; set; }
    }
}
