using MediatR;
using SUTH.HealthCheckup.Application.Features.RolesReportTemplate.ViewModels;

namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.Queries.GetRole
{
    public class GetRoleReportTemplateQuery : IRequest<RolesReportTemplateViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
