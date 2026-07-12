using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class RunningConfiguration : IEntityTypeConfiguration<Running>
{
    public void Configure(EntityTypeBuilder<Running> builder)
    {

        builder.HasKey(x => x.Code);
        builder.Property(x => x.Code).HasMaxLength(10);
    }
}
