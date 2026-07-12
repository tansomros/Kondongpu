using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations
{
    public class ReportGroupConfiguration : IEntityTypeConfiguration<ReportGroup>
    {
        public void Configure(EntityTypeBuilder<ReportGroup> entity)
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Name).IsRequired(true).HasMaxLength(100).HasComment("ชื่อกลุ่มรายงาน");
            entity.Property(d => d.Sort).HasComment("ลำดับการแสดงผล");
            entity.Property(d => d.IsActive).IsRequired(true).HasDefaultValue(true).HasComment("สถานะการเปิดใช้งาน");
           // entity.Property(d => d.CreateDatetime).HasColumnType("timestamp");
            entity.Property(d => d.CreateUser).HasMaxLength(10).HasComment("รหัสผู้ใช้งานที่สร้างรายงาน");
           // entity.Property(d => d.ModifiedDatetime).HasColumnType("timestamp");
            entity.Property(d => d.ModifiedUser).HasMaxLength(10).HasComment("รหัสผู้ใช้งานที่แก้ไขรายงาน");

            entity.HasIndex(d => d.IsActive);

        }
    }
}
