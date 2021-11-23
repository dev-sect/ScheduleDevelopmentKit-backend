using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.DataAccess.EntityConfigurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.Property(e => e.Value).HasMaxLength(60);
        }
    }
}