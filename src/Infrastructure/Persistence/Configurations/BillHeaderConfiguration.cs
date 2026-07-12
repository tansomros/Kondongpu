using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class BillHeaderConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.BillNumber).HasMaxLength(20);
        builder.Property(x => x.CompanyCode).HasMaxLength(20);
        builder.Property(x => x.Remark).HasMaxLength(100);
    }
}
