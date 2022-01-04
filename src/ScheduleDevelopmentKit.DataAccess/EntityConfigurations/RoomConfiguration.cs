using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleDevelopmentKit.Domain.Entities;

namespace ScheduleDevelopmentKit.DataAccess.EntityConfigurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder.OwnsOne(r => r.Number).Property(rn => rn.Value).HasMaxLength(10);
        }
    }
}