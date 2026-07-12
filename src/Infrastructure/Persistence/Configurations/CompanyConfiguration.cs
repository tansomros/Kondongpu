using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasMaxLength(20);
        builder.Property(x => x.Name).HasMaxLength(400);
        builder.Property(x => x.AliasName).HasMaxLength(50);
        builder.Property(x => x.OwnerName).HasMaxLength(100);
        builder.Property(x => x.VatId).HasMaxLength(100);
        builder.Property(x => x.AddressNo).HasMaxLength(100);
        builder.Property(x => x.Moo).HasMaxLength(100);
        builder.Property(x => x.Village).HasMaxLength(100);
        builder.Property(x => x.SubDistrict).HasMaxLength(100);
        builder.Property(x => x.District).HasMaxLength(100);
        builder.Property(x => x.ZipCode).HasMaxLength(10);
        builder.Property(x => x.Telephone).HasMaxLength(100);
        builder.Property(x => x.Fax).HasMaxLength(100);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.Website).HasMaxLength(100);
    }
}
