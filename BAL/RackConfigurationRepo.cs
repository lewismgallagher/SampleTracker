using BAL.DTOs;
using DAL.Data.Entities;
using DAL.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class RackConfigurationRepo
    {
        private readonly ISampleTrackerDbContext _context;
        public RackConfigurationRepo(ISampleTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<RackConfigurationDTO> GetRack(int id)
        {
            return await _context.Racks.ToDTOs().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RackConfigurationDTO>> GetRacks()
        {
            return await _context.Racks.Where(r => r.Deleted != true).ToDTOs().ToListAsync();
        }

        public async Task<int> SaveChanges(RackConfigurationDTO editedRack)
        {
            if (editedRack.Id == 0)
            {
                Rack rack = new Rack() { RackName = "TestName", NumberOfRows = 5, NumberOfColumns = 10 };
                _context.Racks.Add(rack);
            }
            else
            {
                var rackToEdit = await _context.Racks.FirstOrDefaultAsync();
                rackToEdit.RackName = editedRack.RackName;
                rackToEdit.NumberOfColumns = editedRack.NumberOfColumns;
                rackToEdit.NumberOfRows = editedRack.NumberOfRows;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRack(int Id)
        {
            Rack rackToDelete = await _context.Racks.FirstOrDefaultAsync(r => r.Id == Id);

            rackToDelete.Deleted = true;

            return await _context.SaveChangesAsync();
        }

    }
}
