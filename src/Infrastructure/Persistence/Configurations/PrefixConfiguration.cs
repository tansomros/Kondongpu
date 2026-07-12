using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class PrefixConfiguration : IEntityTypeConfiguration<Prefix>
{
    public void Configure(EntityTypeBuilder<Prefix> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(20);
        builder.Property(x => x.Name).HasMaxLength(100);
    }
}
