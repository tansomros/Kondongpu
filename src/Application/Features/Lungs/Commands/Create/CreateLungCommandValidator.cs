using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Lungs.Commands.Create
{
    public class CreateLungCommandValidator : AbstractValidator<CreateLungCommand>
    {
        private readonly KondongpuDatabaseContext _context;
        public  CreateLungCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(p => p.CheckupId).NotNull().WithMessage("Checkup Id ต้องไม่ว่าง");
            RuleFor(p => p.CheckupItemId)
                    .NotEmpty().WithMessage("Checkup ItemId ไม่สามารถเป็นค่าว่างได้")
                    .NotNull().WithMessage("โปรดระบุ Checkup ItemId");
          
        }
    }
}
