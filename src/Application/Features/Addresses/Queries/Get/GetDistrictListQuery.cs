using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Addresses.ViewModel;

namespace Kondongpu.Application.Features.Addresses.Queries.Get;

public record GetDistrictListQuery : IRequest<DistrictListViewModel>
{
    public required string ProvinceId { get; set; }
}

public class GetDistrictListQueryHandler : IRequestHandler<GetDistrictListQuery,DistrictListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetDistrictListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DistrictListViewModel> Handle(GetDistrictListQuery request, CancellationToken cancellationToken)
    {
        var districts =  await _context.Districts
            .Include(c => c.Province)
            .AsNoTracking()
            .Where(d => d.ProvinceId==request.ProvinceId)
            .OrderByDescending(x => x.Name) 
            .ToListAsync(cancellationToken);

        var districtsList = _mapper.Map<List<DistrictViewModel>>(districts);

        return new DistrictListViewModel()
        {
            Districts = districtsList
        };
    }
}
