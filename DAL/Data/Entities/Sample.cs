using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Entities
{
    public class Sample : EntityBase
    {
        public string IdentifyingValue { get; set; }

        public SampleType SampleType { get; set; }
        public int SampleTypeId { get; set; }
        public int RackId { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }


    }
}
