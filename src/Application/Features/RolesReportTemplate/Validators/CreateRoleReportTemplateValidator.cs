using FluentValidation;
using SUTH.HealthCheckup.Application.Features.RolesReportTemplate.Commands;

namespace SUTH.HealthCheckup.Application.Features.RolesReportTemplate.Validators
{
    public class CreateRoleReportTemplateValidator : AbstractValidator<CreateRoleReportTemplateCommand>
    {
        public CreateRoleReportTemplateValidator() 
        {
            RuleFor(p => p.roleReportTemplates.Select(r => r.RoleName))
                  .NotNull()
                  .NotEmpty()
                  .WithMessage("Role Name ไม่สามารถเป็นค่าว่างได้");
        }
    }
}
