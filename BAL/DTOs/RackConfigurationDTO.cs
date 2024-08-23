using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public static class RackConfigurationDTOExtensions
    {
        public static IQueryable<RackConfigurationDTO> ToRackConfigurationDTOs(this IQueryable<Rack> source)
        {
            return source.Select(r => new RackConfigurationDTO
            {
                Id = r.Id,
                RackName = r.RackName,
                NumberOfColumns = r.NumberOfColumns,
                NumberOfRows = r.NumberOfRows,
            });
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
