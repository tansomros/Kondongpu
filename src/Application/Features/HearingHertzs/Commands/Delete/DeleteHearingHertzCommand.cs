using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.HearingHertzs.Commands.Delete;

public record DeleteHearingHertzCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteHearingHertzCommandHandler : IRequestHandler<DeleteHearingHertzCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteHearingHertzCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteHearingHertzCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.HearingHertzs
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(HearingHertz), request.Id);

        _context.HearingHertzs.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}
