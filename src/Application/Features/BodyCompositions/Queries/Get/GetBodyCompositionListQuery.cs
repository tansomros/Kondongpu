using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.BodyCompositions.ViewModels;

namespace Kondongpu.Application.Features.BodyCompositions.Queries.Get;

public class GetBodyCompositionListQuery : IRequest<BodyCompositionListViewModel>
{

}

public class GetBodyCompositionListQueryHandler : IRequestHandler<GetBodyCompositionListQuery, BodyCompositionListViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetBodyCompositionListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BodyCompositionListViewModel> Handle(GetBodyCompositionListQuery request, CancellationToken cancellationToken)
    {
        var bodyCompositions = await _context.BodyCompositions.AsNoTracking().ToListAsync(cancellationToken);
        var bodyCompositionList = _mapper.Map<List<BodyCompositionViewModel>>(bodyCompositions);
        return new BodyCompositionListViewModel()
        {
            BodyCompositions = bodyCompositionList,
        };
    }
}
