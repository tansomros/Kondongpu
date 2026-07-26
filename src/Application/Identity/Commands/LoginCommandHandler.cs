using Kondongpu.Application.Identity.Interfaces;
namespace Kondongpu.Application.Identity.Commands;
public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(
            request.Username,
            request.Password,
            cancellationToken);
    }
}
