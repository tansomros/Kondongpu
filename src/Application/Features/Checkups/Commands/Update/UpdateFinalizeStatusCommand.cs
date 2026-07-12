using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Commands.Update;

public record UpdateFinalizeStatusCommand : IRequest<Unit>
{
    public required string VisitNumber { get; set; }
    public required bool Finalized { get; set; }
}

public class UpdateFinalizeStatusCommandHandler : IRequestHandler<UpdateFinalizeStatusCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IDateTime _dateTime;

    public UpdateFinalizeStatusCommandHandler(
        KondongpuDatabaseContext context, 
        IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<Unit> Handle(UpdateFinalizeStatusCommand request, CancellationToken cancellationToken)
    {
        var checkup = await _context
            .Checkups
            .FirstOrDefaultAsync(c => c.VisitNumber == request.VisitNumber, cancellationToken);

        if (checkup == null)
        {
            throw new NotFoundException(nameof(Checkup), request.VisitNumber);
        }
       
        checkup.IsFinalized = request.Finalized;
        checkup.FinalizedDate = _dateTime.Now.DateTime.ToUniversalTime();

        _context.Checkups.Update(checkup);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
