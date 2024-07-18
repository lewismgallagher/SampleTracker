using DAL.Data.Entities;
using DAL.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public Task<int> SaveChanges()
        {
            Rack rack = new Rack() { RackName = "TestName", NumberOfRows = 5, NumberOfColumns = 10 };
            _context.Racks.Add(rack);
            return _context.SaveChangesAsync();
        }

    }
}
