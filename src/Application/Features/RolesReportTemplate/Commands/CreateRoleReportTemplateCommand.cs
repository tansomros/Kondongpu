using AutoMapper;
using MediatR;

using SUTH.HealthCheckup.Application.Features.RolesReportTemplate.ViewModels;
using SUTH.HealthCheckup.Domain.Entities;

namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.Commands
{
    public class CreateRoleReportTemplateCommand : IRequest<int>
    {
        public List<RolesReportTemplateViewModel> roleReportTemplates { get; set; }
        //public CreateRoleReportTemplateCommand()
        //{
        //    roleReportTemplates = new List<RolesReportTemplateViewModel>();
        //}
    }
    internal class CreateRoleReportTemplateIdCommandHandler : IRequestHandler<CreateRoleReportTemplateCommand, int>
    {
        private readonly ICheckupDatabaseContext _context;
        private readonly IMapper _mapper;
        public CreateRoleReportTemplateIdCommandHandler(ICheckupDatabaseContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateRoleReportTemplateCommand request, CancellationToken cancellationToken)
        {
            var roleReport = _mapper.Map<List<RoleReportTemplate>>(request.roleReportTemplates);
            //List<RoleReportTemplate> roleReport = new List<RoleReportTemplate>();
            //foreach(var role in request.roleReportTemplates)
            //{
            //    roleReport.Add(new RoleReportTemplate
            //    {
            //        RoleName = role.RoleName,

            //        CreateDatetime = role.CreateDateTime,
            //        CreateUser = role.CreateBy,
            //        ModifiedDatetime = role.ModifyDateTime,
            //        ModifiedUser = role.ModifyBy,
            //    });
            //}
            await _context.RoleReportTemplate.AddRangeAsync(roleReport);
            return await _context.SaveChangesAsync(cancellationToken);            
        }
    }
}
