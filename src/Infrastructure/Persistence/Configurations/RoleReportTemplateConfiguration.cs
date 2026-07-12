using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations
{
    public class RoleReportTemplateConfiguration : IEntityTypeConfiguration<RoleReportTemplate>
    {
        public void Configure(EntityTypeBuilder<RoleReportTemplate> entity)
        {
            entity.HasKey(p=>p.Id);            

            entity.Property(d=>d.RoleName).IsRequired(true).HasComment("ชื่อกลุ่มรายงาน");
            entity.Property(d => d.ReportTemplateList).HasColumnType("json").HasComment("กลุ่มรหัสรายงาน Ref. ReportTemplate.Id");
            entity.Property(d => d.IsActive).HasDefaultValue(true).HasComment("สถานะการเปิดใช้งาน");
            entity.Property(d => d.CreatedOn).HasComment("วันที่สร้างรายการ");
            entity.Property(d => d.CreateUser).HasMaxLength(10).HasComment("รหัสผู้ใช้งานที่สร้างรายการ");
            entity.Property(d => d.LastModified).HasComment("วันที่แก้ไขรายการ");
            entity.Property(d => d.ModifiedUser).HasMaxLength(10).HasComment("รหัสผู้ใช้งานที่แก้ไขล่าสุด");

            entity.HasIndex(d => d.Id);
            entity.HasIndex(d=>d.IsActive);
        }
    }
}
