using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.CheckupClasses.Commands.Create
{
    public class CreateCheckupClassCommandValidator : AbstractValidator<CreateCheckupClassCommand>
    {
        private readonly KondongpuDatabaseContext _context;

        public CreateCheckupClassCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;
            RuleFor(p => p.Code).NotEmpty().WithMessage("Code ต้องไม่ว่าง");
            RuleFor(p => p.Name).NotNull().WithMessage("Sort name ต้องไม่ว่าง");
        }
    }
}
