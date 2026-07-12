using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.Lungs.Commands.Update
{
    public class UpdateLungCommandValidator : AbstractValidator<UpdateLungCommand>
    {
        private readonly KondongpuDatabaseContext _context;
        public UpdateLungCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Id ไม่สามารถเป็นค่าว่างได้")
                .NotNull().WithMessage("โปรดระบุ Id")
                .MustAsync(BeExistAsync).WithMessage("ไม่พบข้อมูล")
                .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");
        }
        public async Task<bool> BeExistAsync(int id, CancellationToken cancellationToken)
        {
            var checkup = await _context
                .Checkups
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            return checkup != null;
        }

        public async Task<bool> AvailableToUpdateAsync(int id, CancellationToken cancellationToken)
        {
            var checkup = await _context
                .Checkups
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            return checkup != null && checkup.IsFinalized != true;
        }
    }
}
