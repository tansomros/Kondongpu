using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Visions.Commands.Create
{
    public class CreateVisionCommandValidator : AbstractValidator<CreateVisionCommand>
    {
        private readonly KondongpuDatabaseContext _context;
        public  CreateVisionCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(p => p.CheckupId).NotNull().WithMessage("Checkup Id ต้องไม่ว่าง");
            RuleFor(p => p.CheckupItemId)
                    .NotEmpty().WithMessage("Checkup ItemId ไม่สามารถเป็นค่าว่างได้")
                    .NotNull().WithMessage("โปรดระบุ Checkup ItemId");
            //RuleFor(p => p.VisionRightResult)
            //      .NotEmpty().WithMessage("ผลการตรวจตาขวา ไม่สามารถเป็นค่าว่างได้")
            //      .NotNull().WithMessage("โปรดระบุ ผลการตรวจตาขวา");
            //RuleFor(p => p.VisionLeftResult)
            //      .NotEmpty().WithMessage("ผลการตรวจตาซ้าย ไม่สามารถเป็นค่าว่างได้")
            //      .NotNull().WithMessage("โปรดระบุ ผลการตรวจตาซ้าย");
            //RuleFor(p => p.ResultNote)
            //  .NotEmpty().WithMessage("สรุปผลการตรวจตา ไม่สามารถเป็นค่าว่างได้")
            //  .NotNull().WithMessage("โปรดระบุ สรุปผลการตรวจตา");
            RuleFor(p => p.IsActive)
                    .NotEmpty().WithMessage("สถานะ ไม่สามารถเป็นค่าว่างได้")
                    .NotNull().WithMessage("โปรดระบุ สถานะ");
          
        }
    }
}
