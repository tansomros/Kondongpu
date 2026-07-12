using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Security;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Systems.Commands;
[Authorize(Policy = KondongpuPolicies.AllowAnonymous)]
public class RunningConfigDataInitializerCommand : IRequest<Unit> { }
public class RunningConfigDataInitializerCommandHandler : IRequestHandler<RunningConfigDataInitializerCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public RunningConfigDataInitializerCommandHandler(IKondongpuDatabaseContext kondongpuDatabaseContext)
    {
        _context = kondongpuDatabaseContext;
    }

    public async Task<Unit> Handle(RunningConfigDataInitializerCommand request, CancellationToken cancellationToken)
    {
        await SeedRunningConfigs(cancellationToken); 
        return Unit.Value;
    }

    private async Task SeedRunningConfigs(CancellationToken cancellationToken)
    {
        if (await _context.RunningConfigs.AnyAsync(cancellationToken))
        {
            return;
        }

        var RunningConfig = new[]
        {
            new RunningConfig("C","รหัสลูกค้า",true,false,4),
            new RunningConfig("F","โรงงาน",true,false,2),
            new RunningConfig("K","เลขที่ใบเสร็จรับเงิน",true,true,5),
        };

        await _context.RunningConfigs.AddRangeAsync(RunningConfig, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }     
} 
