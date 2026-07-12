using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.CheckupItems.Commands.Create
{
    public class CreateCheckupItemCommandValidator : AbstractValidator<CreateCheckupItemCommand>
    {
        private readonly KondongpuDatabaseContext _context;

        public CreateCheckupItemCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(p => p.Code).NotNull().WithMessage("Code ต้องไม่ว่าง");
            RuleFor(p => p.DisplayName).NotNull().WithMessage("Sort name ต้องไม่ว่าง");
        }
    }
}
