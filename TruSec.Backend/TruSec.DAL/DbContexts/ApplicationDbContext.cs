using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TruSec.DAL.Entities;

namespace TruSec.DAL.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckDataLog> TruckDataLogs { get; set; }
        public DbSet<TruckSecret> TruckSecrets { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
    }
}
