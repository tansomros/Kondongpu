using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Xrays.Commands.Create;

public record CreateXrayCommand : IRequest<int>
{
    public int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public int CheckupItemId { get; set; }
    public string? ResultValue { get; set; }
    public string? IsAbnormal { get; set; }
    public string? ResultReport { get; set; }
    public required string ReportText { get; set; }
    public required string AccessionNumber { get; set; }
}

public class CreateXrayCommandValidator : AbstractValidator<CreateXrayCommand>
{
    private int? _checkupId;
    private int? _checkupItemId; 
    private readonly KondongpuDatabaseContext _context;

    public CreateXrayCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.VisitNumber).NotEmpty().WithMessage("VisitNumber เป็นค่าว่างไม่ได้");

        RuleFor(p => p.CheckupItemId)
            .NotEmpty().WithMessage("CheckupItemId ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("CheckupItemId ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupItemExistAsync).WithMessage($"ไม่พบรายการ CheckupItem หมายเลข {_checkupItemId} ที่ท่านระบุ");

        //RuleFor(p => p.ReportText).NotEmpty().WithMessage("ReportText เป็นค่าว่างไม่ได้");

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

public class CreateXrayCommandHandler : IRequestHandler<CreateXrayCommand, int>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateXrayCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateXrayCommand request, CancellationToken cancellationToken)
    {
        var xray = new Xray(
            request.CheckupId,
            request.VisitNumber,
            request.CheckupItemId,
            request.ResultValue,
            request.ReportText,
            request.AccessionNumber
            )
        {
            IsAbnormal = request.IsAbnormal,
            ResultReport = request.ResultReport,
        };


        await _context.Xrays.AddAsync(xray, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return xray.Id;
    }
}
