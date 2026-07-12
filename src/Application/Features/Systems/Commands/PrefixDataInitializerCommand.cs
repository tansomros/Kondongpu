using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Security;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Systems.Commands;
[Authorize(Policy = KondongpuPolicies.AllowAnonymous)]
public class PrefixDataInitializerCommand : IRequest<Unit> { }
public class PrefixDataInitializerCommandHandler : IRequestHandler<PrefixDataInitializerCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public PrefixDataInitializerCommandHandler(IKondongpuDatabaseContext kondongpuDatabaseContext)
    {
        _context = kondongpuDatabaseContext;
    }

    public async Task<Unit> Handle(PrefixDataInitializerCommand request, CancellationToken cancellationToken)
    {
        await SeedPrefixs(cancellationToken); 
        return Unit.Value;
    }

    private async Task SeedPrefixs(CancellationToken cancellationToken)
    {
        if (await _context.Prefixs.AnyAsync(cancellationToken))
        {
            return;
        }

        var Prefix = new[]
        {
            new Prefix(1,"นาย"),
            new Prefix(2,"นาง"),          
            new Prefix(3,"นางสาว"), 
        };

        await _context.Prefixs.AddRangeAsync(Prefix, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }     
} 
