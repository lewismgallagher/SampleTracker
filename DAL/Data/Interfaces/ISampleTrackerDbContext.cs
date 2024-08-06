﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DAL.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Data.Interfaces
{
    public interface ISampleTrackerDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<Rack> Racks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();

    }
}
