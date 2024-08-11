using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public static class SampleTypeConfigurationDTOExtensions
    {
        public static IQueryable<SampleTypeConfigurationDTO> ToSampleTypeConfigurationDTOs(this IQueryable<SampleType> source)
        {
            return source.Select(s => new SampleTypeConfigurationDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            });
        }

        public static SampleTypeConfigurationDTO ToSampleTypeConfigurationDTO(SampleType sampleType)
        {
            return new SampleTypeConfigurationDTO()
            {
                Id = sampleType.Id,
                Name = sampleType.Name,
                Description = sampleType.Description,
            };
        }
    }
    public class SampleTypeConfigurationDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        public string? Description { get; set; }
    }
}
