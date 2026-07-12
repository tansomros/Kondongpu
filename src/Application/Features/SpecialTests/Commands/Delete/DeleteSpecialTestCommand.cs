using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.SpecialTests.Commands.Delete;

public record DeleteSpecialTestCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteCommandHandler : IRequestHandler<DeleteSpecialTestCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSpecialTestCommand request, CancellationToken cancellationToken)
    {
        var specialTest = await _context.SpecialTests.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(SpecialTest), request.Id);

        _context.SpecialTests.Remove(specialTest);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}
