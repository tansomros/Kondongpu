using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.Visions.Commands.Update;
public class UpsertVisionCommandValidator : AbstractValidator<UpsertVisionCommand>
{
    private readonly KondongpuDatabaseContext _context;

    public UpsertVisionCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(x => x.CheckupClassCode)
            .NotEmpty()
            .WithMessage("CheckupClassCode ไม่สามารถเป็นค่าว่างได้")
            .NotNull().WithMessage("โปรดระบุ CheckupClassCode")
            .MustAsync(BeExistAsync)
            .WithMessage("CheckupClassCode:ไม่พบข้อมูล CheckupItem");

        RuleFor(x => x.CheckupId)
            .MustAsync(AvailableToUpdateAsync)
            .WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

    }

    public async Task<bool> BeExistAsync(string checkupClassCode, CancellationToken cancellationToken)
    {
        var checkupItems = await _context
            .CheckupItems.AsNoTracking().Include(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Where(c => c.CheckupGroup.CheckupClass.Code == checkupClassCode)
            .ToListAsync(cancellationToken);

        return checkupItems.Count > 0;
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
