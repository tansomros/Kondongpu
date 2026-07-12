using Kondongpu.Application.Common.Security;

namespace Kondongpu.Application.Features.Lookups;

[Authorize(Policy = KondongpuPolicies.AllowAnonymous)]
public record GetLookupCategoriesQuery : IRequest<List<string>>;

public class GetLookupCategoriesQueryHandler : IRequestHandler<GetLookupCategoriesQuery, List<string>>
{
    public Task<List<string>> Handle(GetLookupCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(LookupRegistry.Categories);
    }
}
