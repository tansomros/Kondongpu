using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.NickName).HasMaxLength(100);
        builder.Property(x => x.Sex).HasMaxLength(1);
        builder.Property(x => x.CardId).HasMaxLength(13);
        builder.Property(x => x.FamerId).HasMaxLength(10);
        builder.Property(x => x.Tel).HasMaxLength(50);
        builder.Property(x => x.AddressNo).HasMaxLength(100);
        builder.Property(x => x.Moo).HasMaxLength(100);
        builder.Property(x => x.Village).HasMaxLength(200);
        builder.Property(x => x.SubDistrict).HasMaxLength(100);
        builder.Property(x => x.District).HasMaxLength(100);
        builder.Property(x => x.Zipcode).HasMaxLength(5);
        builder.Property(x => x.AccountNumber).HasMaxLength(50);
        builder.Property(x => x.AccountName).HasMaxLength(100);
    }
}
