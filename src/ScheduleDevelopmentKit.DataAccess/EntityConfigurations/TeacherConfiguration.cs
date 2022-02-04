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

            var nameConfigurationBuilder =  builder.OwnsOne(t => t.Name);
            nameConfigurationBuilder.Property(pn => pn.FirstName).HasMaxLength(30);
            nameConfigurationBuilder.Property(pn => pn.LastName).HasMaxLength(30);
            nameConfigurationBuilder.Property(pn => pn.MiddleName).HasMaxLength(30);

            builder.OwnsOne(t => t.Email).Property(p => p.Value).HasMaxLength(60);
            builder.OwnsOne(t => t.PhoneNumber);
        }
    }
}