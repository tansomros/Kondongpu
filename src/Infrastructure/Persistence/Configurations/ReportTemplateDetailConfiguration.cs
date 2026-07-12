using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Infrastructure.Persistence.Configurations
{
    public class ReportTemplateDetailConfiguration : IEntityTypeConfiguration<ReportTemplateDetail>
    {
        public void Configure(EntityTypeBuilder<ReportTemplateDetail> entity)
        {
            entity.HasKey(f => f.Id);

            entity.Property(d => d.ReportTemplateId).IsRequired(true).HasComment("รหัสอ้างอิงเลขลำดับของรายงาน");
            entity.Property(d => d.ParameterName).HasMaxLength(20).HasComment("ชื่อพารามีเตอร์");
            entity.Property(d => d.ControlType).HasMaxLength(30).HasComment("ประเภทของ control");
            entity.Property(d => d.Description).HasMaxLength(30).HasComment("รายละเอียด");
            entity.Property(d => d.ValueMember).HasMaxLength(20).HasComment("ชื่อค่าที่ต้องการอ้างอิง");
            entity.Property(d => d.DisplayMember).HasMaxLength(50).HasComment("ชื่อค่าที่ต้องการอ้างอิง");
            entity.Property(d => d.SqlText).HasColumnType("text").HasComment("คำสั่ง sql");
            entity.Property(d => d.IsActive).IsRequired(true).HasDefaultValue(true).HasComment("สถานะการเปิดใช้งาน");
            //entity.Property(d => d.CreateDatetime).HasColumnType("timestamp");
            entity.Property(d => d.CreateUser).HasMaxLength(10).HasComment("รหัสผู้ใช้งานที่สร้างรายงาน");
            //entity.Property(d => d.ModifiedDatetime).HasColumnType("timestamp");
            entity.Property(d => d.ModifiedUser).HasMaxLength(10).HasComment("รหัสผู้ใช้งานที่แก้ไขรายงาน");

            entity.HasIndex(d => d.ReportTemplateId);
            entity.HasIndex(d => d.IsActive);
        }
    }
}
