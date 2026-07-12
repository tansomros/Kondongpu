using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.PhysicalExaminations.Commands.Create;
public class CreatePhysicalExaminationCommandValidator : AbstractValidator<CreatePhysicalExaminationCommand>
{
    private readonly KondongpuDatabaseContext _checkupDatabaseContext;
    public CreatePhysicalExaminationCommandValidator(KondongpuDatabaseContext checkupDatabaseContext)
    {
        _checkupDatabaseContext = checkupDatabaseContext;

        RuleFor(p => p.CheckupId).NotNull().NotEmpty().WithMessage("Checkup Id ไม่สามารถเป็นค่าว่างได้")
            .WithMessage("Checkup Id ต้องไม่ว่าง")
            .MustAsync(BeExistCheckupAsync).WithMessage("ไม่พบข้อมูล")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.CheckupItemId)
                .NotEmpty().WithMessage("Checkup ItemId ไม่สามารถเป็นค่าว่างได้")
                .NotNull().WithMessage("โปรดระบุ Checkup ItemId")
                .MustAsync(BeExistCheckItemAsync).WithMessage("ไม่พบข้อมูล");

        RuleFor(p => p.VisitNumber)
              .NotEmpty().WithMessage("Visit Number ไม่สามารถเป็นค่าว่างได้")
              .NotNull().WithMessage("โปรดระบุ Visit Number");
        //RuleFor(p => p.IsAbnormal)
        //      .NotEmpty().WithMessage("Result Abnormal ไม่สามารถเป็นค่าว่างได้")
        //      .NotNull().WithMessage("โปรดระบุ Result Abnormal");

        //RuleFor(p => p.Recommend)
        //.NotEmpty().WithMessage("คำแนะนำ ไม่สามารถเป็นค่าว่างได้")
        //.NotNull().WithMessage("โปรดระบุ คำแนะนำ");
    }
    public async Task<bool> BeExistCheckupAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _checkupDatabaseContext
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null;
    }
    public async Task<bool> BeExistCheckItemAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _checkupDatabaseContext
            .CheckupItems
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null;
    }
    public async Task<bool> AvailableToUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _checkupDatabaseContext
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}
