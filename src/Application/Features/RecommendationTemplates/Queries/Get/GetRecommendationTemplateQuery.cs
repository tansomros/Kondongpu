using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.RecommendationTemplates.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.RecommendationTemplates.Queries.Get;
public class GetRecommendationTemplateQuery : IRequest<RecommendationTemplateViewModel>
{
    public required int Id { get; set; }
}

public class GetRecommendationTemplateQueryHandler : IRequestHandler<GetRecommendationTemplateQuery, RecommendationTemplateViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _checkupContext;

    public GetRecommendationTemplateQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
    {
        _checkupContext = checkupContext;
        _mapper = mapper;
    }

    public async Task<RecommendationTemplateViewModel> Handle(GetRecommendationTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await _checkupContext.RecommendationTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (template == null)
        {
            throw new NotFoundException(nameof(RecommendationTemplate), request.Id);
        }

        return _mapper.Map<RecommendationTemplateViewModel>(template);
    }
}
