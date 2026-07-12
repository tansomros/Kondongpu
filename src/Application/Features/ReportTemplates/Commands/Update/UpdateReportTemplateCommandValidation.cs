using FluentValidation;

namespace Kondongpu.Application.Features.ReportTemplates.Commands.Update
{
    public class UpdateReportTemplateCommandValidation : AbstractValidator<UpdateReportTemplateCommand>
    {
        public UpdateReportTemplateCommandValidation()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty().WithMessage("ว่างไม่ได้");
            RuleFor(r => r.ReportName).NotNull().NotEmpty().WithMessage("ว่างไม่ได้");
            RuleFor(r => r.ReportGroupId).NotNull().NotEmpty().WithMessage("ว่างไม่ได้");        
        }
    }
}
