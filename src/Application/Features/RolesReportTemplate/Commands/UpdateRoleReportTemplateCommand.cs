using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SUTH.HealthCheckup.Application.Common.Interfaces;
using SUTH.HealthCheckup.Application.Features.RolesReportTemplate.ViewModels;

namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.Commands
{
    public class UpdateRoleReportTemplateCommand : IRequest<int>
    {
        public List<RolesReportTemplateViewModel> roleReportTemplates { get; set; }
        public UpdateRoleReportTemplateCommand()
        {
            roleReportTemplates = new List<RolesReportTemplateViewModel>();
        }
    }
    internal class UpdateRoleReportTemplateCommandHandler : IRequestHandler<UpdateRoleReportTemplateCommand, int>
    {
        private readonly ICheckupDatabaseContext _context;
        private readonly IMapper _mapper;
        public UpdateRoleReportTemplateCommandHandler(ICheckupDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateRoleReportTemplateCommand request, CancellationToken cancellationToken)
        {
            var roleIds = request.roleReportTemplates.Select(x=>x.Id).ToList();
            var roleReport = await _context.RoleReportTemplate.Where(x=>roleIds.Contains(x.Id)).ToListAsync(cancellationToken);
            if (roleReport.Any())
            {
                foreach (var role in roleReport)
                {
                    var rs = request.roleReportTemplates.First(x => x.Id == role.Id);
                    role.RoleName =rs.RoleName;
                    role.ModifiedUser = rs.ModifyBy;
                    role.ReportTemplateIds = rs.ReportTemplateIds;
                    role.IsActive = rs.Active;                    
                }
                _context.RoleReportTemplate.UpdateRange(roleReport);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
