using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Xrays.ViewModel;

namespace Kondongpu.Application.Features.Xrays.Queries.Get;

public record GetXrayListQuery : IRequest<XrayListViewModel>
{
    public required string visitNumber { get; set; }
}

public class GetXrayListQueryHandler : IRequestHandler<GetXrayListQuery, XrayListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetXrayListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<XrayListViewModel> Handle(GetXrayListQuery request, CancellationToken cancellationToken)
    {
        //return await _context.Xrays.AsNoTracking()
        //    .Include(xray => xray.CheckupItem)
        //    .OrderByDescending(x => x.CreatedOn)
        //    .ProjectTo<XrayViewModel>(_mapper.ConfigurationProvider)
        //    .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);


        var xrays = await _context.Xrays.AsNoTracking()
            .Include(xray => xray.CheckupItem)
            .ThenInclude(item => item.CheckupGroup)
            .ThenInclude(group => group.CheckupClass)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync(cancellationToken);


        var xraysModel = _mapper.Map<List<XrayViewModel>>(xrays);
        return new XrayListViewModel { Xrays = xraysModel };
    }
}
