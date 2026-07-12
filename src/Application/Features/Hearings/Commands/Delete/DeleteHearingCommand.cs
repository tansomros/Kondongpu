using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Hearings.Commands.Delete;

public record DeleteHearingCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}


public class DeleteHearingCommandHandler : IRequestHandler<DeleteHearingCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteHearingCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteHearingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Hearings
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Hearing), request.Id);

        _context.Hearings.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}
