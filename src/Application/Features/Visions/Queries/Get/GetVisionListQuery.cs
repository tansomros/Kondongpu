using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Visions.ViewModels;

namespace Kondongpu.Application.Features.Visions.Queries.Get;

public class GetVisionListQuery : IRequest<VisionListViewModel>
{

}

public class GetVisionListQueryHandler : IRequestHandler<GetVisionListQuery, VisionListViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetVisionListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VisionListViewModel> Handle(GetVisionListQuery request, CancellationToken cancellationToken)
    {
        var visions = await _context.Visions.AsNoTracking().ToListAsync(cancellationToken);
        var visionList = _mapper.Map<List<VisionViewModel>>(visions);
        return new VisionListViewModel()
        {
            Visions = visionList,
        };
    }
}
