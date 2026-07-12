using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupItems.Commands.Update;
public class UpdateCheckupItemCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string DisplayName { get; set; }
    public string? HosxpICode { get; set; }
    public string? Description { get; set; }
    public string? CumulativeName { get; set; }
    public string? CumulativeGroup { get; set; }
    public required int CheckupGroupId { get; set; } 
    public int Sort { get; set; } 
}

public class UpdateCheckupItemCommandHandler : IRequestHandler<UpdateCheckupItemCommand, Unit>
{
    private readonly KondongpuDatabaseContext _checkupDatabaseContext;

    public UpdateCheckupItemCommandHandler(KondongpuDatabaseContext checkupDatabaseContext)
    {
        _checkupDatabaseContext = checkupDatabaseContext;
    }

    public async Task<Unit> Handle(UpdateCheckupItemCommand request, CancellationToken cancellationToken)
    {
        var checkupItem = await _checkupDatabaseContext
            .CheckupItems
            .FirstOrDefaultAsync(ci => ci.Id == request.Id, cancellationToken);

        if (checkupItem == null)
        {
            throw new NotFoundException(nameof(CheckupItem), request.Id);
        }

        var checkupGroup = await _checkupDatabaseContext
            .CheckupGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(cg => cg.Id == request.CheckupGroupId, cancellationToken);

        if (checkupGroup != null)
        {
            checkupItem.CheckupGroupId = checkupGroup.Id;
        }

        checkupItem.Code = request.Code;
        checkupItem.DisplayName = request.DisplayName;
        checkupItem.LabItemCode = request.HosxpICode;
        checkupItem.Description = request.Description;
        checkupItem.CumulativeName = request.CumulativeName;
        checkupItem.CumulativeGroup = request.CumulativeGroup;
        checkupItem.CheckupGroupId = request.CheckupGroupId;
        checkupItem.Sort = request.Sort;

        _checkupDatabaseContext.CheckupItems.Update(checkupItem);
        await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
