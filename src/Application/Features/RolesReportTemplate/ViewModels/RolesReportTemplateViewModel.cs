using SUTH.HealthCheckup.Domain.Entities;
using System.ComponentModel;


namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.ViewModels
{
    public class RolesReportTemplateViewModel : IPropertyMapping<RoleReportTemplate>
    {
        public Int64 Id { get; set; }
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
        public List<int>? ReportTemplateIds { get; set; }
        public DateTimeOffset CreateDateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTimeOffset ModifyDateTime { get; set; }
        public string ModifyBy { get; set; }
        public bool Active { get; set; }
        public string Flag { get; set; } = "";

        //public RolesReportTemplateViewModel()
        //{
        //    ReportTemplateIds = new List<int>();
        //}
        public void Mapping(AutoMapper.Profile profile)
        {            
            profile.CreateMap<RoleReportTemplate, RolesReportTemplateViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.RoleName, opt => opt.MapFrom(s => s.RoleName))                
                .ForMember(d => d.ReportTemplateIds, opt => opt.MapFrom(s => s.ReportTemplateIds))
                .ForMember(d => d.CreateDateTime, opt => opt.MapFrom(s => s.CreateDatetime))
                .ForMember(d => d.CreateBy, opt => opt.MapFrom(s => s.CreateUser))
                .ForMember(d => d.ModifyDateTime, opt => opt.MapFrom(s => s.ModifiedDatetime))
                .ForMember(d => d.ModifyBy, opt => opt.MapFrom(s => s.ModifiedUser))
                .ForMember(d => d.Active, opt => opt.MapFrom(s => s.IsActive))
                .ReverseMap();
        }
    }
}
