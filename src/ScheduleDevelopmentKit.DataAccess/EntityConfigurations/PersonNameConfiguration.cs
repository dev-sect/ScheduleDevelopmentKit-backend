using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.DataAccess.EntityConfigurations
{
    public class PersonNameConfiguration : IEntityTypeConfiguration<PersonName>
    {
        public void Configure(EntityTypeBuilder<PersonName> builder)
        {
            builder.Property(pn => pn.FirstName).HasMaxLength(30);
            builder.Property(pn => pn.LastName).HasMaxLength(30);
            builder.Property(pn => pn.MiddleName).HasMaxLength(30);
        }
    }
}