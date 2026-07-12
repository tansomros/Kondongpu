using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Commands.Delete;

public record DeleteCheckupCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteCheckupCommandHandler : IRequestHandler<DeleteCheckupCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteCheckupCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCheckupCommand request, CancellationToken cancellationToken)
    {
        var visit = await _context.Checkups.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new Exceptions.NotFoundException(nameof(Checkup), request.Id);

        _context.Checkups.Remove(visit);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
