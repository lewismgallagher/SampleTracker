using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public static class RackDTOExtensions
    {
        public static IQueryable<RackDTO> ToRackDTOs(this IQueryable<Rack> source)
        {
            return source.Select(r => new RackDTO
            {
                Id = r.Id,
                RackName = r.RackName,
                NumberOfColumns = r.NumberOfColumns,
                NumberOfRows = r.NumberOfRows,
            });
        }

        public static RackDTO ToRackDTO(Rack rack)
        {
            return new RackDTO()
            {
                Id = rack.Id,
                RackName = rack.RackName,
                NumberOfColumns = rack.NumberOfColumns,
                NumberOfRows = rack.NumberOfRows,
            };
        }
    }
    public class RackDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name Is Required")]
        public string? RackName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Rows cannot be smaller than 1")]
        public int NumberOfRows { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Columns cannot be smaller than 1")]
        public int NumberOfColumns { get; set; }
  
    }
}
