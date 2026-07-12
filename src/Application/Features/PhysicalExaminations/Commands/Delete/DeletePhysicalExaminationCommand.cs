using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.Commands.Delete;
using Kondongpu.Application.Features.Checkups.Constants;

namespace Kondongpu.Application.Features.PhysicalExaminations.Commands.Delete;

public record DeletePhysicalExaminationCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}
public class DeletePhysicalExaminationCommandValidator : AbstractValidator<DeletePhysicalExaminationCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public DeletePhysicalExaminationCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id ต้องไม่เป็นค่าว่าง")
           .MustAsync(BeExistAsync).WithMessage("ไม่พบข้อมูล")
           .MustAsync(AvailableToDeleteAsync).WithMessage("ไม่สามารถลบข้อมูลได้เนื่องจากไม่อยู่ในสถานะที่สามารถลบข้อมูลได้");
         
    }
    public async Task<bool> BeExistAsync(int id, CancellationToken cancellationToken)
    {
        var checkup = await _context
            .PhysicalExams
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return checkup != null;
    }
    public async Task<bool> AvailableToDeleteAsync(int id, CancellationToken cancellationToken)
    {
        var physicalExams = await _context
            .PhysicalExams .AsNoTracking().Select(p => new { p.CheckupId, p.Id })
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        
        int checkupId = physicalExams?.CheckupId ?? 0;
        var checkup = await _context
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == checkupId, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}
public class DeleteCommandHandler : IRequestHandler<DeletePhysicalExaminationCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePhysicalExaminationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PhysicalExams
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(PhysicalExaminations), request.Id);

        _context.PhysicalExams.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
