using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Security;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Systems.Commands;
[Authorize(Policy = KondongpuPolicies.AllowAnonymous)]
public class RoleDataInitializerCommand : IRequest<Unit> { }
public class RoleDataInitializerCommandHandler : IRequestHandler<RoleDataInitializerCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public RoleDataInitializerCommandHandler(IKondongpuDatabaseContext kondongpuDatabaseContext)
    {
        _context = kondongpuDatabaseContext;
    }

    public async Task<Unit> Handle(RoleDataInitializerCommand request, CancellationToken cancellationToken)
    {
        await SeedRoles(cancellationToken); 
        return Unit.Value;
    }

    private async Task SeedRoles(CancellationToken cancellationToken)
    {
        if (await _context.Roles.AnyAsync(cancellationToken))
        {
            return;
        }

        var Role = new[]
        {
            new Role(1,"ADMINISTRATOR"),
            new Role(2,"OFFICER"),          
            new Role(3,"MANAGER"),
            new Role(4,"USER"),
        };

        await _context.Roles.AddRangeAsync(Role, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }     
} 
