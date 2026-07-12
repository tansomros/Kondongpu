using SUTH.HealthCheckup.Application.Common.Interfaces;
using SUTH.HealthCheckup.Application.Features.RolesReportTemplate.ViewModels;

namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.Queries
{
    public class GetRolesReportTemplateListQuery : IRequest<List<RolesReportTemplateViewModel>>
    {
    }
    internal class GetRoleReportTemplateListHandler : IRequestHandler<GetRolesReportTemplateListQuery, List<RolesReportTemplateViewModel>>
    {
        private readonly ICheckupDatabaseContext _context;
        private readonly IMapper _mapper;
        public GetRoleReportTemplateListHandler(ICheckupDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<RolesReportTemplateViewModel>> Handle(GetRolesReportTemplateListQuery request, CancellationToken cancellationToken)
        {
            var role = await _context.RoleReportTemplate.AsNoTracking()
                .Where(d => d.IsActive)
                .ProjectTo<RolesReportTemplateViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(o=>o.Id)
                .ToListAsync(cancellationToken);
            return role;
        }
    }
}
