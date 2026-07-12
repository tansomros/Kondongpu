using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Security;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Systems.Commands;
[Authorize(Policy = KondongpuPolicies.AllowAnonymous)]
public class CaneTypeDataInitializerCommand : IRequest<Unit> { }
public class CaneTypeDataInitializerCommandHandler : IRequestHandler<CaneTypeDataInitializerCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public CaneTypeDataInitializerCommandHandler(IKondongpuDatabaseContext kondongpuDatabaseContext)
    {
        _context = kondongpuDatabaseContext;
    }

    public async Task<Unit> Handle(CaneTypeDataInitializerCommand request, CancellationToken cancellationToken)
    {
        await SeedCaneTypes(cancellationToken); 
        return Unit.Value;
    }

    private async Task SeedCaneTypes(CancellationToken cancellationToken)
    {
        if (await _context.CaneTypes.AnyAsync(cancellationToken))
        {
            return;
        }

        var CaneType = new[]
        {
            new CaneType("อ้อยสด"),
            new CaneType("อ้อยไฟไหม้"),
        };

        await _context.CaneTypes.AddRangeAsync(CaneType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }     
} 
