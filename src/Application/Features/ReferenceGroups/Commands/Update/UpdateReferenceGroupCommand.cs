using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

#pragma warning disable CS0618
namespace Kondongpu.Application.Features.ReferenceGroups.Commands.Update;
[Obsolete("ใช้ SmartEnum จาก Domain.Enums แทน — ดู LookupRegistry.cs")]
public class UpdateReferenceGroupCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Descriptions { get; set; }
    public int Sort { get; set; }
}

[Obsolete("ใช้ SmartEnum จาก Domain.Enums แทน — ดู LookupRegistry.cs")]
public class UpdateReferenceGroupCommandHandler : IRequestHandler<UpdateReferenceGroupCommand, Unit>
{
    private readonly KondongpuDatabaseContext _checkupContext;
    public UpdateReferenceGroupCommandHandler(KondongpuDatabaseContext checkupContext)
    {
        _checkupContext = checkupContext;
    }

    public async Task<Unit> Handle(UpdateReferenceGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _checkupContext.ReferenceGroups
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (group == null)
        {
            throw new NotFoundException(nameof(ReferenceGroup), request.Id);
        }

        group.Code = request.Code;
        group.Descriptions = request.Descriptions;
        group.Sort = request.Sort;

        _checkupContext.ReferenceGroups.Update(group);
        await _checkupContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
