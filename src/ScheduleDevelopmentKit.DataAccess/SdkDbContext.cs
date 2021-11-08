using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.DataAccess.EntityConfigurations;
using ScheduleDevelopmentKit.Domain.Entities;

namespace ScheduleDevelopmentKit.DataAccess
{
    public class SdkDbContext : DbContext
    {
        public SdkDbContext(DbContextOptions<SdkDbContext> options) : base(options) {}

        public DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TeacherConfiguration());
        }
    }
}