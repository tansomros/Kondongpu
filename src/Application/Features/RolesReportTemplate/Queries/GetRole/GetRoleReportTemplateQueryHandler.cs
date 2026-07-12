
using SUTH.HealthCheckup.Application.Common.Interfaces;
using SUTH.HealthCheckup.Application.Exceptions;
using SUTH.HealthCheckup.Application.Features.RolesReportTemplate.ViewModels;
using SUTH.HealthCheckup.Domain.Entities;

namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.Queries.GetRole
{
    public class GetRoleReportTemplateQueryHandler : IRequestHandler<GetRoleReportTemplateQuery, RolesReportTemplateViewModel>
    {
        private readonly ICheckupDatabaseContext _context;
        private readonly IMapper _mapper;
        public GetRoleReportTemplateQueryHandler(ICheckupDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<RolesReportTemplateViewModel> Handle(GetRoleReportTemplateQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new NotImplementedException();
        
            var    role = await _context.RoleReportTemplate
                    .Where(d => d.Id.Equals(request.Id) ||  d.RoleName.ToLower().Equals(request.Name.ToLower().Trim()) && d.IsActive.Equals("A"))
                    .SingleOrDefaultAsync(cancellationToken);
                if (role == null)
                    throw new NotFoundException(nameof(RoleReportTemplate), request.Id);
           
            return _mapper.Map<RolesReportTemplateViewModel>(role);
        }
    }
}
