using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Entities
{    
    public class Rack : EntityBase

    {
        public string RackName { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }

        public ICollection<Samples> Samples { get; set; }

    }
}
