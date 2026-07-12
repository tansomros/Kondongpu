using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Xrays.ViewModel;

namespace Kondongpu.Application.Features.Xrays.Queries.Get;

public record GetXrayListWithClassQuery : IRequest<XrayListViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetXrayListWithClassQueryHandler : IRequestHandler<GetXrayListWithClassQuery, XrayListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetXrayListWithClassQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<XrayListViewModel> Handle(GetXrayListWithClassQuery request, CancellationToken cancellationToken)
    {
        var xrays = await _context.Xrays
            .AsNoTracking()
            .Include(l => l.CheckupItem!).ThenInclude(i => i.CheckupGroup).ThenInclude(g => g.CheckupClass).AsNoTracking()
            .Where(l => l.VisitNumber == request.VisitNumber && l.CheckupItem != null )
            .ToListAsync(cancellationToken);
                
        var xrayList = _mapper.Map<List<XrayViewModel>>(xrays);

        return new XrayListViewModel() { Xrays = xrayList };
    }
}
