using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Labs.Commands.Update;

public record UpdateLabCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required int CheckupId { get; set; }
    public string? ResultValue { get; set; }
    public string? ReferenceRange { get; set; }
    public string? IsAbnormal { get; set; }
    public string? Comments { get; set; }
    public DateOnly? ResultDate { get; set; }
    public TimeOnly ResultTime { get; set; }
}

public class UpdateLabCommandValidator : AbstractValidator<UpdateLabCommand>
{
    private int? _id;
    private int? _checkupId;
    private readonly KondongpuDatabaseContext _context;
    public UpdateLabCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Id ต้องไม่เป็นค่า NULL")
            .MustAsync(LabExistAsync).WithMessage($"ไม่พบรายการ Lab หมายเลข {_id} ที่ท่านระบุ");

        RuleFor(p => p.ResultValue).NotEmpty().WithMessage("ResultValue ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.ReferenceRange).NotEmpty().WithMessage("ReferenceRange ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.IsAbnormal).NotEmpty().WithMessage("IsAbnormal ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.Comments).NotEmpty().WithMessage("Comments ต้องไม่เป็นค่าว่าง");

        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupIdExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ")
            .MustAsync(AvailableToUpdateAsync).WithMessage("ไม่สามารถปรับปรุงข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถปรับปรุงข้อมูลได้");
    }

    public async Task<bool> LabExistAsync(int labId, CancellationToken cancellationToken)
    {
        _id = labId;
        return await _context.Labs.AsNoTracking().AnyAsync(h => h.Id == labId, cancellationToken);
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

public class UpdateLabCommandHandler : IRequestHandler<UpdateLabCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateLabCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateLabCommand request, CancellationToken cancellationToken)
    {
        var lab = await _context.Labs.FirstOrDefaultAsync(a => a.Id == request.Id)
            ?? throw new NotFoundException(nameof(Lab), request.Id);


        lab.ResultValue = request.ResultValue;
        lab.ReferenceRange = request.ReferenceRange;
        lab.IsAbnormal = request.IsAbnormal;
        lab.Comments = request.Comments;
        lab.ResultDate = request.ResultDate;
        lab.ResultTime = request.ResultTime;

        _context.Labs.Update(lab);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
