using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DAL.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Data.Interfaces
{
    public interface ISampleTrackerDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DatabaseFacade Database { get; }

         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
