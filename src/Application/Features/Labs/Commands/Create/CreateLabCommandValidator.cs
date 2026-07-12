using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.Labs.Commands.Create;
public class CreateLabCommandValidator : AbstractValidator<CreateLabCommand>
{
    private int? _checkupId;
    private string? _labTtemCode;
    private readonly KondongpuDatabaseContext _context;
    public CreateLabCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.VisitNumber).NotEmpty().WithMessage("VisitNumber ไม่สามารถเป็นค่าว่างได้");

        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupIdExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.LabItemCode)
            .NotEmpty().WithMessage("CheckupItemId ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("CheckupItemId ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupItemIdExistAsync).WithMessage($"ไม่พบรายการ CheckupItem หมายเลข {_labTtemCode} ที่ท่านระบุ");

        RuleFor(p => p.ResultValue).NotEmpty().WithMessage("ResultValue ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.ReferenceRange).NotEmpty().WithMessage("ReferenceRange ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.IsAbnormal).NotEmpty().WithMessage("IsAbnormal ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.Comments).NotEmpty().WithMessage("Comments ต้องไม่เป็นค่าว่าง");
    }

    public async Task<bool> CheckupIdExistAsync(int checkupId, CancellationToken cancellationToken)
    {
        _checkupId = checkupId;
        return await _context.Checkups.AsNoTracking().AnyAsync(c => c.Id == checkupId, cancellationToken);
    }

    public async Task<bool> CheckupItemIdExistAsync(string labItemCode, CancellationToken cancellationToken)
    {
        _labTtemCode = labItemCode;
        return await _context.CheckupItems.AsNoTracking().AnyAsync(c => c.LabItemCode == labItemCode, cancellationToken);
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
