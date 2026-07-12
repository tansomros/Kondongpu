using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Recommendations.ViewModels;

namespace Kondongpu.Application.Features.Recommendations.Queries.Get;

public class GetRecommendationListQuery : IRequest<RecommendationListViewModel>
{

}

public class GetRecommendationListQueryHandler : IRequestHandler<GetRecommendationListQuery, RecommendationListViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetRecommendationListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RecommendationListViewModel> Handle(GetRecommendationListQuery request, CancellationToken cancellationToken)
    {
        var lungs = await _context.Recommendations.AsNoTracking().ToListAsync(cancellationToken);
        var lungList = _mapper.Map<List<RecommendationViewModel>>(lungs);
        return new RecommendationListViewModel()
        {
            Recommendations = lungList,
        };
    }
}
