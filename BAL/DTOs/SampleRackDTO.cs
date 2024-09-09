using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public static class SampleRackDTOExtensions
    {
        public static IQueryable<SampleRackDTO> ToSampleRackDTOs(this IQueryable<Rack> source)
        {
            return source.Select(r => new SampleRackDTO
            {
                Id = r.Id,
                RackName = r.RackName,
                NumberOfRows = r.NumberOfRows,
                NumberOfColumns = r.NumberOfColumns,
            });
            
        }

    }

    public class SampleRackDTO
    {
        public int Id { get; set; }

        public string? RackName { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }

    }
}
