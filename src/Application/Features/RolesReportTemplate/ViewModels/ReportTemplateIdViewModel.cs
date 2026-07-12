
using AutoMapper;

using SUTH.HealthCheckup.Domain.ValueObjects;

namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.ViewModels
{
    public class ReportTemplateIdViewModel : IPropertyMapping<ReportTemplateId>
    {
        public string ReportId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReportTemplateId, ReportTemplateIdViewModel>()
                .ForMember(d => d.ReportId, opt => opt.MapFrom(s => s.ReportId))
                .ReverseMap();
        }
    }
}
