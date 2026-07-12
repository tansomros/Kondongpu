using Kondongpu.Application.Common.Extensions;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.RecommendationTemplates.ViewModels;

namespace Kondongpu.Application.Features.RecommendationTemplates.Queries.GetPaginatedList;
public class GetRecommendationTemplatePaginatedListQuery : IRequest<PaginatedList<RecommendationTemplateViewModel>>
{
    public string? SearchTerm { get; set; }
    public required int Page { get; set; }
    public required int Length { get; set; }
}

public class GetRecommendationTemplatePaginatedListQueryHandler
    : IRequestHandler<GetRecommendationTemplatePaginatedListQuery, PaginatedList<RecommendationTemplateViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetRecommendationTemplatePaginatedListQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<RecommendationTemplateViewModel>> 
        Handle(GetRecommendationTemplatePaginatedListQuery request, CancellationToken cancellationToken)
    {
        var query = _context
            .RecommendationTemplates
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Filter(request.SearchTerm);
        }

        query = query
            .OrderByDescending(d => d.LastModified)
            .ThenBy(d => d.CreatedOn);

        var count = await query.CountAsync(cancellationToken);
        var filtered = await query
            .AsNoTracking()
            .Skip((request.Page - 1) * request.Length)
            .Take(request.Length)
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<List<RecommendationTemplateViewModel>>(filtered);
        return new PaginatedList<RecommendationTemplateViewModel>(viewModels, count, request.Page, request.Length);
    }
}
