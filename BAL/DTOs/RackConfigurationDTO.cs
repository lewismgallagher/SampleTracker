using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public static class RackConfigurationDTOExtensions
    {
        public static IQueryable<RackConfigurationDTO> ToDTOs(this IQueryable<Rack> source)
        {
            return source.Select(r => new RackConfigurationDTO
            {
                Id = r.Id,
                RackName = r.RackName,
                NumberOfColumns = r.NumberOfColumns,
                NumberOfRows = r.NumberOfRows,
            });
        }

        public static RackConfigurationDTO ToDTO(Rack rack)
        {
            return new RackConfigurationDTO()
            {
                Id = rack.Id,
                RackName = rack.RackName,
                NumberOfColumns = rack.NumberOfColumns,
                NumberOfRows = rack.NumberOfRows,
            };
        }
    }
    public class RackConfigurationDTO
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
