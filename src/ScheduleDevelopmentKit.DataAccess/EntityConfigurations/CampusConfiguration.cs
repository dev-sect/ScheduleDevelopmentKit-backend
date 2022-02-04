using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleDevelopmentKit.Domain.Entities;

namespace ScheduleDevelopmentKit.DataAccess.EntityConfigurations
{
    public class CampusConfiguration : IEntityTypeConfiguration<Campus>
    {
        public void Configure(EntityTypeBuilder<Campus> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();

            builder.OwnsOne(c => c.Name).Property(cn => cn.Value).HasMaxLength(100);
            builder.OwnsOne(c => c.Address).Property(a => a.Value).HasMaxLength(300);
            builder.Navigation(c => c.Rooms).HasField("_rooms");
            builder.HasMany(c => c.Rooms).WithOne(r => r.Campus);
        }
    }
}