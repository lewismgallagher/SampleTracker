using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Entities
{
    public class Samples : EntityBase
    {
        public string IdentifyingValue { get; set; }

        public SampleTypes SampleType { get; set; }
        public int SampleTypeId { get; set; }

    }
}
