using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Category : Component
    {
        public virtual List<Component> Components { get; set; }
    }
}
