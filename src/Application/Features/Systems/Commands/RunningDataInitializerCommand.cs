using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Security;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Systems.Commands;
[Authorize(Policy = KondongpuPolicies.AllowAnonymous)]
public class RunningDataInitializerCommand : IRequest<Unit> { }
public class RunningDataInitializerCommandHandler : IRequestHandler<RunningDataInitializerCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public RunningDataInitializerCommandHandler(IKondongpuDatabaseContext kondongpuDatabaseContext)
    {
        _context = kondongpuDatabaseContext;
    }

    public async Task<Unit> Handle(RunningDataInitializerCommand request, CancellationToken cancellationToken)
    {
        await SeedRunnings(cancellationToken); 
        return Unit.Value;
    }

    private async Task SeedRunnings(CancellationToken cancellationToken)
    {
        if (await _context.Runnings.AnyAsync(cancellationToken))
        {
            return;
        }

        var Running = new[]
        {
            new Running("C",0,0),
            new Running("F",0,0),
            new Running("K",69,0),
        };

        await _context.Runnings.AddRangeAsync(Running, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }     
} 
