using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public static class SampleDTOExtensions
    {
        public static IQueryable<SampleDTO> ToSampleDTOs(this IQueryable<Sample> source)
        {
            return source.Select(s => new SampleDTO
            {
                Id = s.Id,
                IdentifyingValue = s.IdentifyingValue,
                RowNumber = s.RowNumber,
                ColumnNumber = s.ColumnNumber,
                SampleType = s.SampleType.Name,
                SampleTypeId = s.SampleTypeId
            });
        }

        //public static IQueryable<SampleDTO> ToSampleDTOList(this IQueryable<Sample> source)
        //{
        //    return source.Select(s => new SampleDTO
        //    {
        //        Id = s.Id,
        //        IdentifyingValue = s.IdentifyingValue,
        //        RowNumber = s.RowNumber,
        //        ColumnNumber = s.ColumnNumber,
        //        SampleType = s.SampleType.Name,
        //        SampleTypeId = s.SampleTypeId
        //    });
        //}
    }

    public class SampleDTO
    {
        public int Id { get; set; }
        public int? SampleTypeId { get; set; }
        public string IdentifyingValue { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string SampleType { get; set; }
    }
}
