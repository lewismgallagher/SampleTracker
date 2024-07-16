using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DAL.Data.Entities;

namespace DAL.Data.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
