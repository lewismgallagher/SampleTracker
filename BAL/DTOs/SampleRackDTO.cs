using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public static class SampleRackDTOExtensions
    {
        public static IQueryable<SampleRackDTO> ToSampleRackDTOs(this IQueryable<Rack> source)
        {
            return source.Select(r => new SampleRackDTO
            {
                RackId = r.Id,
                RackName = r.RackName,
                NumberOfRows = r.NumberOfRows,
                NumberOfColumns = r.NumberOfColumns,
                Samples = r.Samples.ToSampleDTOList()
            });
            
        }

        //public static SampleRackDTO ToSampleRackDTO(Rack rack)
        //{
        //    return new SampleRackDTO()
        //    {
        //        RackId = rack.Id,
        //        RackName = rack.RackName,
        //        NumberOfRows = rack.NumberOfRows,
        //        NumberOfColumns = rack.NumberOfColumns,
        //        Samples = rack.Samples.ToSampleDTOList()
        //    };
        //}
    }

    public class SampleRackDTO
    {
        public int RackId { get; set; }

        public string? RackName { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }

        public ICollection<SampleDTO> Samples { get; set;}
    }
}
