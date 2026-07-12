using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Lungs.ViewModels;

namespace Kondongpu.Application.Features.Lungs.Queries.Get;

public class GetLungListQuery : IRequest<LungListViewModel>
{

}

public class GetLungListQueryHandler : IRequestHandler<GetLungListQuery, LungListViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetLungListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LungListViewModel> Handle(GetLungListQuery request, CancellationToken cancellationToken)
    {
        var lungs = await _context.Lungs.AsNoTracking().ToListAsync(cancellationToken);
        var lungList = _mapper.Map<List<LungViewModel>>(lungs);
        return new LungListViewModel()
        {
            Lungs = lungList,
        };
    }
}
