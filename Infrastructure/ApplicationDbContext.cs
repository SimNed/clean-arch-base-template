using Domain.Plants;
using Domain.DomainUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Plant> Plant => Set<Plant>();
        public DbSet<DomainUser> DomainUser => Set<DomainUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Configurations.PlantConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.DomainUserConfiguration());
        }
    }
}
