using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.RecommendationTemplates.ViewModels;

namespace Kondongpu.Application.Features.RecommendationTemplates.Queries.Get;

public record GetRecommendationTemplateListQuery : IRequest<RecommendationTemplateListViewModel>
{
  
}

public class GetRecommendationTemplateListQueryHandler : IRequestHandler<GetRecommendationTemplateListQuery,RecommendationTemplateListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetRecommendationTemplateListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RecommendationTemplateListViewModel> Handle(GetRecommendationTemplateListQuery request, CancellationToken cancellationToken)
    {
        var rcmTempate =  await _context.RecommendationTemplates
            .AsNoTracking()
            .OrderByDescending(x => x.Text) 
            .ToListAsync(cancellationToken);

        var textsList = _mapper.Map<List<RecommendationTemplateViewModel>>(rcmTempate);

        return new RecommendationTemplateListViewModel()
        {
            RecommendationTemplates = textsList
        };
    }
}
