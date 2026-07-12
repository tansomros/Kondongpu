using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Audiograms.Commands.Delete;

public record DeleteAudiogramCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteAudiogramCommandHandler : IRequestHandler<DeleteAudiogramCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteAudiogramCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAudiogramCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Audiograms
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Audiogram), request.Id);

        _context.Audiograms.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
