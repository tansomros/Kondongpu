using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupItems.Commands.Delete;
public class DeleteCheckupItemCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteCheckupItemCommandHandler : IRequestHandler<DeleteCheckupItemCommand, Unit>
{
    private readonly KondongpuDatabaseContext _checkupDatabaseContext;

    public DeleteCheckupItemCommandHandler(KondongpuDatabaseContext checkupDatabaseContext)
    {
        _checkupDatabaseContext = checkupDatabaseContext;
    }

    public async Task<Unit> Handle(DeleteCheckupItemCommand request, CancellationToken cancellationToken)
    {
        var checkupItem = await _checkupDatabaseContext
            .CheckupItems
            .FirstOrDefaultAsync(ci => ci.Id == request.Id, cancellationToken);

        if (checkupItem == null)
        {
            throw new NotFoundException(nameof(CheckupItem), request.Id);
        }

        _checkupDatabaseContext.CheckupItems.Remove(checkupItem);
        await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
