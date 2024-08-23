using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public static class SampleDTOExtensions
    {
        public static IQueryable<SampleDTO> ToSampleDTOs(this IQueryable<Sample> source)
        {
            return source.Select(s => new SampleDTO
            {
                Id = s.Id,
                IdentifyingValue = s.IdentifyingValue,
                OriginalIdentifyingValue = s.IdentifyingValue,
                RowNumber = s.RowNumber,
                ColumnNumber = s.ColumnNumber,
                SampleType = s.SampleType.Name,
                SampleTypeId = s.SampleTypeId,
                RackId = s.RackId
                
            });
        }

    }

    public class SampleDTO
    {
        public int Id { get; set; }
        public int SampleTypeId { get; set; }
        public int RackId { get; set; }
        public string IdentifyingValue { get; set; }
        public string OriginalIdentifyingValue { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string SampleType { get; set; }

    }
}
