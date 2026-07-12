using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations;

public class CarRegistrationConfiguration : IEntityTypeConfiguration<CustomerCar>
{
    public void Configure(EntityTypeBuilder<CustomerCar> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.CarNumber).HasMaxLength(50);
    }
}
