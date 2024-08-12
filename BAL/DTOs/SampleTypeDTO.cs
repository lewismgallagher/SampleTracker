using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public static class SampleTypeDTOExtensions
    {
        public static IQueryable<SampleTypeDTO> ToSampleTypeDTOs(this IQueryable<SampleType> source)
        {
            return source.Select(s => new SampleTypeDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            });
        }

    }
    public class SampleTypeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        public string? Description { get; set; }
    }
}
