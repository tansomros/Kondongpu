using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Labs.ViewModel;

namespace Kondongpu.Application.Features.Labs.Queries.Get;

public record GetLabListWithClassQuery : IRequest<LabListViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetLabListWithClassQueryHandler : IRequestHandler<GetLabListWithClassQuery, LabListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetLabListWithClassQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LabListViewModel> Handle(GetLabListWithClassQuery request, CancellationToken cancellationToken)
    {
        var labs = await _context.Labs
            .AsNoTracking()
            .Include(l => l.CheckupItem!).ThenInclude(i => i.CheckupGroup).ThenInclude(g => g.CheckupClass).AsNoTracking()
            .Where(l => l.VisitNumber == request.VisitNumber && l.CheckupItem != null )
            .ToListAsync(cancellationToken);
                
        var labList = _mapper.Map<List<LabViewModel>>(labs);

        return new LabListViewModel() { Labs = labList };
    }
}
