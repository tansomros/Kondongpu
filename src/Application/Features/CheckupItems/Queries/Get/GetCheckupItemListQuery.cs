using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;

namespace Kondongpu.Application.Features.CheckupItems.Queries.Get;
public class GetCheckupItemListQuery : IRequest<CheckupItemListViewModel>
{
    public required List<string> LabOrder { get; set; }
    public required List<string> XrayOrder { get; set; }
    public required List<string> ServiceOrder { get; set; }

}
public class GetCheckupItemListHandler : IRequestHandler<GetCheckupItemListQuery, CheckupItemListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _checkupContext;

    public GetCheckupItemListHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
    {
        _checkupContext = checkupContext;
        _mapper = mapper;
    }

    public async Task<CheckupItemListViewModel> Handle(GetCheckupItemListQuery request, CancellationToken cancellationToken)
    {
        //var xrayOrder = new HashSet<string>(request.XrayOrder, StringComparer.OrdinalIgnoreCase);
        //var labOrder = new HashSet<string>(request.LabOrder, StringComparer.OrdinalIgnoreCase);
        //var serviceOrder = new HashSet<string>(request.ServiceOrder, StringComparer.OrdinalIgnoreCase);

        //var checkupItem = await _context
        //    .CheckupItems
        //    .Include(c => c.CheckupGroup!)
        //        .ThenInclude(c => c.CheckupClass!)
        //    .AsNoTracking()
        //    .Where(
        //    p => p.IsActive && (request.XrayOrder.Contains(p.LabItemCode!) || request.LabOrder.Contains(p.LabItemCode!) || request.ServiceOrder.Contains(p.LabItemCode!))
        //    )
        //    .ToListAsync(cancellationToken);

        List<string> allCodes = new();
        if (request.LabOrder != null && request.LabOrder.Count > 0) allCodes.AddRange(request.LabOrder);
        if (request.XrayOrder != null && request.XrayOrder.Count > 0) allCodes.AddRange(request.XrayOrder);
        if (request.ServiceOrder != null && request.ServiceOrder.Count > 0) allCodes.AddRange(request.ServiceOrder);

        if (allCodes.Count == 0)
        {
            return new CheckupItemListViewModel()
            {
                CheckupItems = []
            };
        }

        var checkupItem = await _checkupContext
            .CheckupItems
            .Include(c => c.CheckupGroup!)
                .ThenInclude(c => c.CheckupClass!)
            .AsNoTracking()
            .Where(p => p.IsActive && allCodes.Contains(p.LabItemCode!))
            .ToListAsync(cancellationToken);

        var checkupItemList = _mapper.Map<List<CheckupItemViewModel>>(checkupItem);

        return new CheckupItemListViewModel()
        {
            CheckupItems = checkupItemList
        };    
    }
}
