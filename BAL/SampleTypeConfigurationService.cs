using Services.DTOs;
using DAL.Data.Entities;
using DAL.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SampleTypeConfigurationService
    {
        private readonly ISampleTrackerDbContext _context;
        public SampleTypeConfigurationService(ISampleTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<SampleTypeConfigurationDTO> GetSampleType(int id)
        {
            return await _context.SampleTypes.ToSampleTypeConfigurationDTOs().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<SampleTypeConfigurationDTO>> GetSampleTypes()
        {
            return await _context.SampleTypes.Where(r => r.Deleted != true).ToSampleTypeConfigurationDTOs().ToListAsync();
        }

        public async Task<bool> SaveChangesAsync(SampleTypeConfigurationDTO editedSampleType)
        {
            if (editedSampleType.Id == 0)
            {
                SampleType sample = new SampleType()
                {
                    Name = editedSampleType.Name,
                    Description = editedSampleType.Description,
                };
                _context.SampleTypes.Add(sample);
            }
            else
            {
                var sampleTypeToEdit = await _context.SampleTypes.FirstOrDefaultAsync(s => s.Id == editedSampleType.Id);
                sampleTypeToEdit.Name = editedSampleType.Name;
                sampleTypeToEdit.Description = editedSampleType.Description;
            }

            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> DeleteSampleType(int Id)
        {
            SampleType sampleTypeToDelete = await _context.SampleTypes.FirstOrDefaultAsync(r => r.Id == Id);

            sampleTypeToDelete.Deleted = true;

            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
