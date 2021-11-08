using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleDevelopmentKit.Domain.Entities;

namespace ScheduleDevelopmentKit.DataAccess.EntityConfigurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();

            builder.OwnsOne(t => t.Name);
            builder.OwnsOne(t => t.Email);
            builder.OwnsOne(t => t.PhoneNumber);
        }
    }
}