using System.Security.Cryptography;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.SpecialTests.Commands.Update;

public record UpdateSpecialTestCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required int CheckupId { get; set; }
    public string? ResultValue { get; set; }
    public string? ReferenceRange { get; set; }
    public string? IsAbnormal { get; set; }
    public string? ResultReport { get; set; }
    public string? Comments { get; set; }
    public DateOnly? ResultDate { get; set; }
    public TimeOnly ResultTime { get; set; }
}

public class UpdateSpecialTestCommandValidator : AbstractValidator<UpdateSpecialTestCommand>
{
    private int? _id;
    private int? _checkupId;
    private readonly KondongpuDatabaseContext _context;
    public UpdateSpecialTestCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Id ต้องไม่เป็นค่า NULL")
            .MustAsync(SpecialTestExistAsync).WithMessage($"ไม่พบรายการ SpecialTest หมายเลข {_id} ที่ท่านระบุ");

        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupIdExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

    }

    public async Task<bool> SpecialTestExistAsync(int specialTestId, CancellationToken cancellationToken)
    {
        _id = specialTestId;
        return await _context.SpecialTests.AsNoTracking().AnyAsync(h => h.Id == specialTestId, cancellationToken);
    }

    public async Task<bool> CheckupIdExistAsync(int checkupId, CancellationToken cancellationToken)
    {
        _checkupId = checkupId;
        return await _context.Checkups.AsNoTracking().AnyAsync(c => c.Id == checkupId, cancellationToken);
    }

    public async Task<bool> AvailableToUpdateAsync(int id, CancellationToken cancellationToken)
    {
        _checkupId = id;
        var checkup = await _context
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}

public class UpdateCommandHandler : IRequestHandler<UpdateSpecialTestCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSpecialTestCommand request, CancellationToken cancellationToken)
    {
        var specialTest = await _context.SpecialTests.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(SpecialTest), request.Id);


        specialTest.ResultValue = request.ResultValue;
        specialTest.ReferenceRange = request.ReferenceRange;
        specialTest.IsAbnormal = request.IsAbnormal;
        specialTest.ResultReport = request.ResultReport;
        specialTest.Comments = request.Comments;
        specialTest.ResultDate = request.ResultDate;
        specialTest.ResultTime = request.ResultTime;

        _context.SpecialTests.Update(specialTest);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}
