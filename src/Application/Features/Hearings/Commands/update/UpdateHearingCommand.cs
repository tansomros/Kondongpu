using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.Hearings.Commands.Update;

public record UpdateHearingCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required int Hertz { get; set; }
    public required double LeftHz { get; set; }
    public required double RightHz { get; set; }
}

public class UpdateHearingCommandValidator : AbstractValidator<UpdateHearingCommand>
{
    private int _hearingId;
    private readonly KondongpuDatabaseContext _context;
    public UpdateHearingCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("AudigramId ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("AudigramId ต้องไม่เป็นค่า NULL")
            .MustAsync(HearingExist).WithMessage($"ไม่พบรายการ Audiogram หมายเลข {_hearingId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.Hertz).NotNull().WithMessage("Hertz เป็นค่าว่างไม่ได้");
        RuleFor(p => p.LeftHz).NotNull().WithMessage("LeftHz เป็นค่าว่างไม่ได้");
        RuleFor(p => p.RightHz).NotNull().WithMessage("RightHz เป็นค่าว่างไม่ได้");
    }

    public async Task <bool> HearingExist(int hearingId, CancellationToken cancellationToken)
    {
        _hearingId = hearingId;
        return await _context.Hearings.AsNoTracking().AnyAsync(h => h.Id == hearingId, cancellationToken);
    }

    public async Task<bool> AvailableToUpdateAsync(int hearingId, CancellationToken cancellationToken)
    {
        var hearing = await _context.Hearings.AsNoTracking().FirstOrDefaultAsync(h => h.Id == hearingId, cancellationToken);
            
        if(hearing == null) return false;

        var audiogram = await _context
            .Audiograms
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == hearing.AudiogramId, cancellationToken);

        if (audiogram == null) return false;

        var checkup = await _context.Checkups.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == audiogram.CheckupId, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}

public class UpdateHearingCommandHandler : IRequestHandler<UpdateHearingCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateHearingCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateHearingCommand request, CancellationToken cancellationToken)
    {
        var hearing = await _context.Hearings.FirstAsync(h => h.Id == request.Id, cancellationToken);

        hearing.Hertz = request.Hertz;
        hearing.LeftHz = request.LeftHz;
        hearing.RightHz = request.RightHz;

        _context.Hearings.Update(hearing);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
