using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.SpecialTests.Commands.Create;

public record CreateSpecialTestCommand : IRequest<int>
{
    public required int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required int CheckupItemId { get; set; }
    public string? ResultValue { get; set; }
    public string? ReferenceRange { get; set; }
    public string? IsAbnormal { get; set; }
    public string? ResultReport { get; set; }
    public string? Comments { get; set; }
    public DateOnly? ResultDate { get; set; }
    public TimeOnly? ResultTime { get; set; }
}

public class CreateSpecialTestCommandValidator : AbstractValidator<CreateSpecialTestCommand>
{
    private int _checkupId;
    private int _checkupItemId;
    private readonly KondongpuDatabaseContext _context;
    public CreateSpecialTestCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;
        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.VisitNumber).NotEmpty().WithMessage("VisitNumber เป็นค่าว่างไม่ได้");

        RuleFor(p => p.CheckupItemId).Must(p => p > 0).WithMessage("CheckupItemId มีค่าน้อยกว่าหรือเท่ากับ 0 ไม่ได้");

        RuleFor(p => p.CheckupItemId)
            .NotEmpty().WithMessage("CheckupItemId ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("CheckupItemId ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupItemExistAsync).WithMessage($"ไม่พบรายการ CheckupItem หมายเลข {_checkupItemId} ที่ท่านระบุ");
    }


    public async Task<bool> CheckupItemExistAsync(int checkupItemId, CancellationToken cancellationToken)
    {
        _checkupItemId = checkupItemId;
        return await _context.CheckupItems.AsNoTracking().AnyAsync(p => p.Id == checkupItemId, cancellationToken);
    }

    public async Task<bool> CheckupExistAsync(int checkupId, CancellationToken cancellationToken)
    {
        _checkupId = checkupId;
        return await _context.Checkups.AsNoTracking().AnyAsync(c => c.Id == checkupId, cancellationToken);
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

public class CreateSpecialTestCommandHandler : IRequestHandler<CreateSpecialTestCommand, int>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateSpecialTestCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSpecialTestCommand request, CancellationToken cancellationToken)
    {
        var specialTest = new SpecialTest(
            request.CheckupId, 
            request.VisitNumber, 
            request.CheckupItemId
            )
        {
            ResultValue = request.ResultValue,
            ReferenceRange = request.ReferenceRange,
            IsAbnormal = request.IsAbnormal,
            ResultReport = request.ResultReport,
            Comments = request.Comments,
            ResultDate = request.ResultDate,
            ResultTime = request.ResultTime,
        };

        await _context.SpecialTests.AddAsync(specialTest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return specialTest.Id;

    }
}
