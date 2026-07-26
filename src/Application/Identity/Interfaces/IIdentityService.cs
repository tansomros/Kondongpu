using Kondongpu.Application.Identity.Commands;

namespace Kondongpu.Application.Identity.Interfaces;
public interface IIdentityService
{
    Task<LoginResponse> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken);
}
