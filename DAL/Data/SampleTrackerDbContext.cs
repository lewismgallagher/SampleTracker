using DAL.Data.Entities;
using DAL.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class SampleTrackerDbContext : DbContext
    {
        public SampleTrackerDbContext(DbContextOptions<SampleTrackerDbContext> options) : base(options)
        { 
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Rack> Racks { get; set; }

        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker
        //        .Entries()
        //        .Where(e => e.Entity is EntityBase && (
        //                e.State == EntityState.Added
        //                || e.State == EntityState.Modified));

        //    foreach (var entityEntry in entries)
        //    {
        //        ((EntityBase)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

        //        if (entityEntry.State == EntityState.Added)
        //        {
        //            ((EntityBase)entityEntry.Entity).CreatedDateTime = DateTime.Now;
        //            ((EntityBase)entityEntry.Entity).Deleted = false;
        //        }
        //    }

        //    return base.SaveChanges();
        //}

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker
        //        .Entries()
        //        .Where(e => e.Entity is EntityBase && (
        //                e.State == EntityState.Added
        //                || e.State == EntityState.Modified));

        //    foreach (var entityEntry in entries)
        //    {
        //        ((EntityBase)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

        //        if (entityEntry.State == EntityState.Added)
        //        {
        //            ((EntityBase)entityEntry.Entity).CreatedDateTime = DateTime.Now;
        //            ((EntityBase)entityEntry.Entity).Deleted = false;
        //        }
        //    }

        //    return base.SaveChangesAsync();
        //}
    }
}