using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Hearings.Commands.Create;

public record CreateHearingCommand : IRequest<int>
{
    public required int AudiogramId { get; set; }
    public required int Hertz { get; set; }
    public required double LeftHz { get; set; }
    public required double RightHz { get; set; }
}

public class CreateHearingCommandValidator : AbstractValidator<CreateHearingCommand>
{
    private int? _idAudiogram;
    private readonly KondongpuDatabaseContext _context;
    public CreateHearingCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;
        RuleFor(p => p.Hertz).NotNull().WithMessage("Hertz เป็นค่าว่างไม่ได้");
        RuleFor(p => p.LeftHz).NotNull().WithMessage("LeftHz เป็นค่าว่างไม่ได้");
        RuleFor(p => p.RightHz).NotNull().WithMessage("RightHz เป็นค่าว่างไม่ได้");

        RuleFor(p => p.AudiogramId)
            .NotEmpty().WithMessage("AudiogramId ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("AudiogramId ต้องไม่เป็นค่า NULL")
            .MustAsync(AudiogramExistAsync).WithMessage($"ไม่พบรายการ Audiogram หมายเลข {_idAudiogram} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");
    }

    public async Task<bool> AudiogramExistAsync(int audiogramId, CancellationToken cancellationToken)
    {
        _idAudiogram = audiogramId;
        return await _context.Audiograms.AsNoTracking().AnyAsync(p => p.Id == audiogramId, cancellationToken);
    }

    public async Task<bool> AvailableToUpdateAsync(int audiogramId, CancellationToken cancellationToken)
    {
        var audiogram = await _context
            .Audiograms
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == audiogramId, cancellationToken);

        if (audiogram == null) return false;

        var checkup = await _context.Checkups.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == audiogram.CheckupId, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}

public class CreateHearingCommandHandler : IRequestHandler<CreateHearingCommand, int>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateHearingCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateHearingCommand request, CancellationToken cancellationToken)
    {
        var hearing = new Hearing(
            request.AudiogramId,
            request.Hertz,
            request.LeftHz,
            request.RightHz
            );

        await _context.Hearings.AddAsync(hearing, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return hearing.Id;

    }
}
