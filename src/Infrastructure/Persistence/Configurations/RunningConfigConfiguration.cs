using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class RunningConfigConfiguration : IEntityTypeConfiguration<Kondongpu.Domain.Entities.RunningConfig>
{
    public void Configure(EntityTypeBuilder<Kondongpu.Domain.Entities.RunningConfig> builder)
    {

        builder.HasKey(x => x.Code);
        builder.Property(x => x.Code).HasMaxLength(20);
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(x => x.IsCode).HasMaxLength(1);
        builder.Property(x => x.IsYear).HasMaxLength(1);
        builder.Property(x => x.TemplateCode).HasMaxLength(100);
    }
}
