using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Users.ViewModel;

namespace Kondongpu.Application.Features.Users.Queries.Get;

public record GetUserLoginQuery : IRequest<UserListViewModel>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class GetUserListWithClassQueryHandler : IRequestHandler<GetUserLoginQuery, UserListViewModel>
{
    private readonly IMapper _mapper;
    private readonly IKondongpuDatabaseContext _context;

    public GetUserListWithClassQueryHandler(IKondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserListViewModel> Handle(GetUserLoginQuery request, CancellationToken cancellationToken)
    {
        var Users = await _context.Users
            .AsNoTracking()            
            .Where(l => l.Username == request.Username && l.PasswordHash== request.Password)
            .ToListAsync(cancellationToken);
                
        var UserList = _mapper.Map<List<UserViewModel>>(Users);

        return new UserListViewModel() { Users = UserList };
    }
}
