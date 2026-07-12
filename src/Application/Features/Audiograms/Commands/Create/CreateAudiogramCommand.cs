using FluentValidation;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Audiograms.Commands.Create;

public record CreateAudiogramCommand : IRequest<int>
{
    public required int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required int CheckupItemId { get; set; }
    public required string LeftNote { get; set; }
    public required string RightNote { get; set; }
    public required string LeftResult { get; set; }
    public required string RightResult { get; set; }
    public required string ResultNote { get; set; }
    public ICollection<CreateHearingAudiogramCommand>? Hearings { get; set; }
}

public class CreateAudiogramCommandValidator : AbstractValidator<CreateAudiogramCommand>
{
    private int? _checkupId;
    private int? _checkupItemId; 
    private readonly KondongpuDatabaseContext _context;
    public CreateAudiogramCommandValidator(KondongpuDatabaseContext context)
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


        RuleFor(p => p.LeftResult).NotEmpty().WithMessage("LeftResult เป็นค่าว่างไม่ได้");
        RuleFor(p => p.RightResult).NotEmpty().WithMessage("RightResult เป็นค่าว่างไม่ได้");
        //RuleFor(p => p.ResultNote).NotEmpty().WithMessage("ResultNote เป็นค่าว่างไม่ได้");
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

    public async Task<bool> AvailableToUpdateAsync(int checkupId, CancellationToken cancellationToken)
    {
        var checkup = await _context
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == checkupId, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}

public class CreateAudiogramCommandHandler : IRequestHandler<CreateAudiogramCommand, int>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateAudiogramCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAudiogramCommand request, CancellationToken cancellationToken)
    {
        var audiogram = new Audiogram(
            request.CheckupId,
            request.VisitNumber,
            request.CheckupItemId,            
            request.LeftResult,
            request.RightResult,
            request.ResultNote,
            request.LeftNote,
            request.RightNote
            
            );

        await _context.Audiograms.AddAsync(audiogram, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return audiogram.Id;
    }
}
