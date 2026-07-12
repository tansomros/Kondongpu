using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Recommendations.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Recommendations.Queries.Get;

public record GetRecommendationByIdQuery : IRequest<RecommendationViewModel>
{
    public required int Id { get; set; }
}

public class GetRecommendationByIdQueryHandler : IRequestHandler<GetRecommendationByIdQuery, RecommendationViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetRecommendationByIdQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RecommendationViewModel> Handle(GetRecommendationByIdQuery request, CancellationToken cancellationToken)
    {
        var recommendation = await _context.Recommendations.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Recommendation), request.Id);

        return _mapper.Map<RecommendationViewModel>(recommendation);
    }
}
