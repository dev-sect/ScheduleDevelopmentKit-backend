using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.Domain.Entities;

namespace ScheduleDevelopmentKit.DataAccess
{
    public class SdkDbContext : DbContext
    {
        public SdkDbContext(DbContextOptions<SdkDbContext> options) : base(options) {
            Database.EnsureCreated();
        }
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Campus> Campuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}