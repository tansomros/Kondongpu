using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Xrays.Commands.Delete;

public record DeleteXrayCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteXrayCommandHandler : IRequestHandler<DeleteXrayCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteXrayCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteXrayCommand request, CancellationToken cancellationToken)
    {
        var xray = await _context.Xrays.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Xray), request.Id);

        _context.Xrays.Remove(xray);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
