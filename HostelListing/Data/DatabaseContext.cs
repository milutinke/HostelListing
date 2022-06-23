using HostelListing.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HostelListing.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Country>? Countries { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Identity Core required
            base.OnModelCreating(builder);

            // User roles
            builder.ApplyConfiguration(new RoleConfiguration());

            // Seeding Countries
            builder.ApplyConfiguration(new CountryConfiguration());

            // Seeding hotels
            builder.ApplyConfiguration(new HotelConfiguration());
        }
    }
}
