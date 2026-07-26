using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Systems.Commands;
public class UserDataInitializerCommand : IRequest<Unit> { }
public class UserDataInitializerCommandHandler : IRequestHandler<UserDataInitializerCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public UserDataInitializerCommandHandler(IKondongpuDatabaseContext kondongpuDatabaseContext)
    {
        _context = kondongpuDatabaseContext;
    }

    public async Task<Unit> Handle(UserDataInitializerCommand request, CancellationToken cancellationToken)
    {
        await SeedUsers(cancellationToken); 
        return Unit.Value;
    }

    private async Task SeedUsers(CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var User = new[]
        {           
            new User("admin","AQAAAAEAACcQAAAAEDJirZQCGuiZ5HI0fDDQNNiCQBSAMXSpy/IHCPizqejnsuxEVe8AMswL16Uy3nDs1g==","Administrator","Admin",1),//4321
            new User("teerapong","AQAAAAEAACcQAAAAEDJirZQCGuiZ5HI0fDDQNNiCQBSAMXSpy/IHCPizqejnsuxEVe8AMswL16Uy3nDs1g==","นาย ธีรพงศ์ ลานอก","ผู้จัดการ",1), 
        };

        await _context.Users.AddRangeAsync(User, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
     
}
