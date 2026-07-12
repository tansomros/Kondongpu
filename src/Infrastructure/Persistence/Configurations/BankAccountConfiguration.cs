using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.AccountNumber).HasMaxLength(20);
        builder.Property(x => x.AccountName).HasMaxLength(100);
        builder.Property(x => x.Branch).HasMaxLength(50);
    }
}
