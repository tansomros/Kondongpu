using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Labs.Commands.Delete;

public record DeleteLabCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}


public class DeleteLabCommandHandler(KondongpuDatabaseContext context) : IRequestHandler<DeleteLabCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context = context;

    public async Task<Unit> Handle(DeleteLabCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Labs
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Lab), request.Id);

        _context.Labs.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
