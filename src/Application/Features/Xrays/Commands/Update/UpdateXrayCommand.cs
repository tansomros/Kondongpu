using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Xrays.Commands.Update;

public record UpdateXrayCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public int CheckupId { get; set; }
    public int CheckupItemId { get; set; }
    public string? ResultValue { get; set; }
    public string? IsAbnormal { get; set; }
    public string? ResultReport { get; set; }
    public string? ReportText { get; set; }
}

public class UpdateCommandValidator : AbstractValidator<UpdateXrayCommand>
{
    private int? _id;
    private int? _checkupId;
    private readonly KondongpuDatabaseContext _context;

    public UpdateCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Id ต้องไม่เป็นค่า NULL")
            .MustAsync(XrayExistAsync).WithMessage($"ไม่พบรายการ Xray หมายเลข {_id} ที่ท่านระบุ");

        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        //RuleFor(p => p.ReportText).NotEmpty().WithMessage("ReportText เป็นค่าว่างไม่ได้");
    }

    public async Task<bool> XrayExistAsync(int xrayId, CancellationToken cancellationToken)
    {
        _id = xrayId;
        return await _context.Xrays.AsNoTracking().AnyAsync(x => x.Id == xrayId, cancellationToken);
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

public class UpdateCommandHandler : IRequestHandler<UpdateXrayCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateXrayCommand request, CancellationToken cancellationToken)
    {
        var xray = await _context.Xrays.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Xray), request.Id);

        xray.ResultValue = request.ResultValue;
        xray.IsAbnormal = request.IsAbnormal;
        xray.ResultReport = request.ResultReport;
        //xray.ReportText = request.ReportText;


        _context.Xrays.Update(xray);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}
