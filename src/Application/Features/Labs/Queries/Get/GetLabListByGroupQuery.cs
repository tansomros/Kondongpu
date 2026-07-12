using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Labs.ViewModel;

namespace Kondongpu.Application.Features.Labs.Queries.Get;

public record GetLabListByGroupQuery : IRequest<LabListViewModel>
{
    public required string VisitNumber { get; set; }
    public required string GroupCode { get; set; }
}

public class GetLabListByGroupQueryHandler : IRequestHandler<GetLabListByGroupQuery, LabListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetLabListByGroupQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LabListViewModel> Handle(GetLabListByGroupQuery request, CancellationToken cancellationToken)
    {
        var labs = await _context.Labs
            .AsNoTracking()
            .Include(l => l.CheckupItem!).ThenInclude(i => i.CheckupGroup).ThenInclude(g => g.CheckupClass).AsNoTracking()
            .Where(l => l.VisitNumber == request.VisitNumber  && l.CheckupItem != null && l.CheckupItem.CheckupGroup.Code == request.GroupCode)
            .ToListAsync(cancellationToken);
                
        var labList = _mapper.Map<List<LabViewModel>>(labs);

        return new LabListViewModel() { Labs = labList };
    }
}
