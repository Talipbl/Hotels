using Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Hotel : IEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Star { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }
        public string Url { get; set; }
    }
}
