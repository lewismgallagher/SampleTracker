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
    public class SampleRackService
    {

        private readonly ISampleTrackerDbContext _context;
        public SampleRackService(ISampleTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckSampleExists(string identifyingValue)
        {
            return await _context.Samples.AnyAsync(s => s.IdentifyingValue == identifyingValue && s.Deleted != true);
        }

        public async Task<int> GetSampleIdByIdentifyingValue(string identifyingvalue)
        {
            return await _context.Samples.Where(s => s.IdentifyingValue == identifyingvalue && s.Deleted != true)
                .Select(s => s.Id).FirstOrDefaultAsync();
        }

        public async Task<SampleDTO> GetSampleByIdentifyingValue(string identifyingvalue)
        {
            return await _context.Samples.Where(s => s.IdentifyingValue == identifyingvalue && s.Deleted != true)
                .ToSampleDTOs().FirstOrDefaultAsync();
        }

        public async Task<List<RackDTO>> SearchRacks(int? id, string name)
        {
            var query = _context.Racks.Where(r => r.Deleted != true);

            if(id != null) query = query.Where(r => r.Id == id); 
            if(!string.IsNullOrWhiteSpace(name)) query = query.Where(r => r.RackName.Contains(name));

            return await query.ToRackDTOs().ToListAsync();
        }

        //Shorten and refactor
        public async Task<SampleRackDTO> GetRack(int id)
        {
            var query =  _context.Racks.Include(r => r.Samples)
                .ThenInclude(s => s.SampleType)
                .ToSampleRackDTOs();
           return await query.FirstOrDefaultAsync(r => r.RackId == id);
        }

        public async Task<List<SampleDTO>> GetRackSamples(int rackId)
        {
            var query = _context.Samples.Where(s => s.RackId == rackId && s.Deleted != true).Include(s => s.SampleType)
                .ToSampleDTOs();
            return await query.ToListAsync();
        }

        public async Task<List<SampleTypeDTO>> GetSampleTypes()
        {
            return await _context.SampleTypes.Where(r => r.Deleted != true).ToSampleTypeDTOs().ToListAsync();
        }

        public async Task<bool> CreateSample(SampleDTO editedSample)
        {
            Sample sample = new Sample()
            {
                IdentifyingValue = editedSample.IdentifyingValue,
                RowNumber = editedSample.RowNumber,
                ColumnNumber = editedSample.ColumnNumber,
                RackId = editedSample.RackId,
                SampleTypeId = editedSample.SampleTypeId
            };
            _context.Samples.Add(sample);

            return await _context.SaveChangesAsync() >= 0;

        }

        public async Task<bool> UpdateSample(SampleDTO editedSample)
        {
            var sampleToEdit = await _context.Samples.FirstOrDefaultAsync(s => s.Id == editedSample.Id);
            sampleToEdit.IdentifyingValue = editedSample.IdentifyingValue;
            sampleToEdit.RowNumber = editedSample.RowNumber;
            sampleToEdit.ColumnNumber = editedSample.ColumnNumber;
            sampleToEdit.RackId = editedSample.RackId;

            return await _context.SaveChangesAsync() >= 0;

        }

        public async Task<bool> SaveChangesAsync(SampleDTO editedSample)
        {
            if (editedSample.Id == 0)
            {
                Sample sample = new Sample()
                {
                    IdentifyingValue = editedSample.IdentifyingValue,
                    RowNumber = editedSample.RowNumber,
                    ColumnNumber = editedSample.ColumnNumber,
                    RackId = editedSample.RackId,
                    SampleTypeId = editedSample.SampleTypeId
                };
                _context.Samples.Add(sample);
            }
            else
            {
                var sampleToEdit = await _context.Samples.FirstOrDefaultAsync(s => s.Id == editedSample.Id);
                sampleToEdit.IdentifyingValue = editedSample.IdentifyingValue;
                sampleToEdit.RowNumber = editedSample.RowNumber;
                sampleToEdit.ColumnNumber = editedSample.ColumnNumber;
                sampleToEdit.RackId = editedSample.RackId;
            }

            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> DeleteSample(int Id)
        {
            Sample sampleToDelete = await _context.Samples.FirstOrDefaultAsync(s => s.Id == Id);

            sampleToDelete.Deleted = true;

            return await _context.SaveChangesAsync() >= 0;
        }


    }
}
