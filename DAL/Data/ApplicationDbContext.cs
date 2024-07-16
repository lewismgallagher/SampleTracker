using DAL.Data.Entities;
using DAL.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
    }
}