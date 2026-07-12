using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Application.Features.Hearings.Commands.Update;

namespace Kondongpu.Application.Features.Audiograms.Commands.Update;

public record UpdateAudiogramCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required int CheckupId { get; set; }
    public required string LeftNote { get; set; }
    public required string RightNote { get; set; }
    public required string LeftResult { get; set; }
    public required string RightResult { get; set; }
    public required string ResultNote { get; set; }
    public ICollection<UpdateHearingCommand>? HearingUpdateCommand { get; set; }

}

public class UpdateAudiogramCommandValidator : AbstractValidator<UpdateAudiogramCommand>
{
    private int _audiogramId { get; set; }
    private int _checkupId;
    private readonly KondongpuDatabaseContext _context;
    public UpdateAudiogramCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Id ต้องไม่เป็นค่า NULL")
            .MustAsync(AudiogramExistAsync).WithMessage($"ไม่พบรายการ Audiogram หมายเลข {_audiogramId} ที่ท่านระบุ");

        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");

        RuleFor(p => p.LeftResult).NotEmpty().WithMessage("LeftResult เป็นค่าว่างไม่ได้");
        RuleFor(p => p.RightResult).NotEmpty().WithMessage("RightResult เป็นค่าว่างไม่ได้");
        RuleFor(p => p.ResultNote).NotEmpty().WithMessage("ResultNote เป็นค่าว่างไม่ได้");


    }

    public async Task<bool> AudiogramExistAsync(int audiogramId, CancellationToken cancellationToken)
    {
        _audiogramId = audiogramId;
        return await _context.Audiograms.AsNoTracking().AnyAsync(c => c.Id == audiogramId, cancellationToken);
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

public class UpdateAudiogramCommandHandler : IRequestHandler<UpdateAudiogramCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateAudiogramCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateAudiogramCommand request, CancellationToken cancellationToken)
    {
        var audiogram = await _context.Audiograms.FirstAsync(a => a.Id == request.Id, cancellationToken);

        audiogram.LeftNote = request.LeftNote;
        audiogram.RightNote = request.RightNote;
        audiogram.LeftResult = request.LeftResult;
        audiogram.RightResult = request.RightResult;
        audiogram.ResultNote = request.ResultNote;

        if (request.HearingUpdateCommand != null)
        {
            foreach (UpdateHearingCommand cmd in request.HearingUpdateCommand)
            {
                var hearing = await _context.Hearings.FirstOrDefaultAsync(
                    h => h.Id == cmd.Id && h.AudiogramId == audiogram.Id
                    , cancellationToken
                    );
                if (hearing != null)
                {
                    hearing.Hertz = cmd.Hertz;
                    hearing.LeftHz = cmd.LeftHz;
                    hearing.RightHz = cmd.RightHz;
                    _context.Hearings.Update(hearing);
                    await _context.SaveChangesAsync(cancellationToken);
                }                
            }
        }

        _context.Audiograms.Update(audiogram);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
