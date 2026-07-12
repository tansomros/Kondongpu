using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.CheckupGroups.Commands.Create
{
    public class CreateCheckupGroupCommandValidator : AbstractValidator<CreateCheckupGroupCommand>
    {
        private readonly KondongpuDatabaseContext _context;

        public CreateCheckupGroupCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;
            RuleFor(p => p.Code).NotNull().WithMessage("Code ต้องไม่ว่าง");
            RuleFor(p => p.Name).NotNull().WithMessage("Sort name ต้องไม่ว่าง");
        }
    }
}
