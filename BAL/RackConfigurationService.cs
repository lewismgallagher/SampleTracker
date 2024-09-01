using Services.DTOs;
using DAL.Data.Entities;
using DAL.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RackConfigurationService
    {
        private readonly ISampleTrackerDbContext _context;
        public RackConfigurationService(ISampleTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<RackConfigurationDTO> GetRack(int id)
        {
            return await _context.Racks.ToRackConfigurationDTOs().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RackConfigurationDTO>> GetRacks()
        {
            return await _context.Racks.Where(r => r.Deleted != true).ToRackConfigurationDTOs().ToListAsync();
        }

        public async Task<Rack> CreateAsync(RackConfigurationDTO editedRack)
        {
            Rack rack = new Rack();

                rack.RackName = editedRack.RackName;
                rack.NumberOfRows = editedRack.NumberOfRows;
                rack.NumberOfColumns = editedRack.NumberOfColumns;
                _context.Racks.Add(rack);
             await _context.SaveChangesAsync();

            return rack;
        }

        public async Task<Rack> Updateasync(RackConfigurationDTO editedRack)
        {
                var rackToEdit = await _context.Racks.FirstOrDefaultAsync(r => r.Id == editedRack.Id);
                rackToEdit.RackName = editedRack.RackName;
                rackToEdit.NumberOfColumns = editedRack.NumberOfColumns;
                rackToEdit.NumberOfRows = editedRack.NumberOfRows;

             await _context.SaveChangesAsync();

            return rackToEdit;
        }

        public async Task<bool> SaveChangesAsync(RackConfigurationDTO editedRack)
        {
            if (editedRack.Id == 0)
            {
                Rack rack = new Rack()
                {
                    RackName = editedRack.RackName,
                    NumberOfRows = editedRack.NumberOfRows,
                    NumberOfColumns = editedRack.NumberOfColumns
                };
                _context.Racks.Add(rack);
            }
            else
            {
                var rackToEdit = await _context.Racks.FirstOrDefaultAsync(r => r.Id == editedRack.Id);
                rackToEdit.RackName = editedRack.RackName;
                rackToEdit.NumberOfColumns = editedRack.NumberOfColumns;
                rackToEdit.NumberOfRows = editedRack.NumberOfRows;
            }

            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> DeleteRack(int Id)
        {
            Rack rackToDelete = await _context.Racks.FirstOrDefaultAsync(r => r.Id == Id);

            rackToDelete.Deleted = true;

            return await _context.SaveChangesAsync() >= 0;
        }

    }
}
