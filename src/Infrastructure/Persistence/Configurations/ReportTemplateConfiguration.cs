using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations
{
    public class ReportTemplateConfiguration : IEntityTypeConfiguration<ReportTemplate>
    {
        public void Configure(EntityTypeBuilder<ReportTemplate> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name).HasMaxLength(200).IsRequired(true).HasComment("ชื่อรายงาน");
            entity.Property(p => p.SqlText).HasColumnType("text").HasComment("คำสั่ง sql");
            //entity.Property(p => p.DatabaseSource).HasMaxLength(20).HasComment("ฐานข้อมูลที่ใช้เรียกรายงาน");
            //entity.Property(p => p.ReportGroupID).HasComment("รหัสกลุ่มของรายงาน Ref. ReportGroup.Id");
            entity.Property(p => p.IsActive).HasDefaultValue(true).HasComment("สถานะการเปิดใช้งาน");
            entity.Property(p => p.CreatedOn).HasComment("วันที่สร้างรายงาน");
            entity.Property(p => p.CreateUser).HasMaxLength(10).IsRequired(true).HasComment("รหัสผู้ใช้งานที่สร้างรายงาน");
            entity.Property(p => p.LastModified).HasComment("วันที่แก้ไขรายงาน");
            entity.Property(p => p.ModifiedUser).HasMaxLength(10).IsRequired(true).HasComment("รหัสผู้ใช้งานที่แก้ไขรายงาน");
            entity.Property(p => p.IsConfidential).HasDefaultValue(false).HasComment("รายงานปกปิด");

            entity.HasIndex(p => p.Id);
            entity.HasIndex(p => p.IsActive);
            entity.HasIndex(p => p.ReportGroupId);
            entity.HasIndex(p => p.IsConfidential);
        }
    }
}
