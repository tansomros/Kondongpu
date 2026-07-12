using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.CheckupItems.Commands.Update;
public class UpdateCheckupItemCommandValidator : AbstractValidator<UpdateCheckupItemCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public UpdateCheckupItemCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Id ต้องไม่เป็นค่า NULL")
            .MustAsync(BeExistAsync).WithMessage("ไม่พบข้อมูล");

        RuleFor(p => p.Code).NotNull().WithMessage("Code ต้องไม่ว่าง");
        RuleFor(p => p.DisplayName).NotNull().WithMessage("Sort name ต้องไม่ว่าง");
    }

    public async Task<bool> BeExistAsync(int id, CancellationToken cancellationToken)
    {
        var checkupItem = await _context
            .CheckupItems
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkupItem != null;
    }
}
