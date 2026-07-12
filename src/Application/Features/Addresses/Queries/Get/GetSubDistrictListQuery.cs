using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Addresses.ViewModel;

namespace Kondongpu.Application.Features.Addresses.Queries.Get;

public record GetSubDistrictListQuery : IRequest<SubDistrictListViewModel>
{
    public required string DistrictId { get; set; }
}

public class GetSubDistrictListQueryHandler : IRequestHandler<GetSubDistrictListQuery, SubDistrictListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetSubDistrictListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SubDistrictListViewModel> Handle(GetSubDistrictListQuery request, CancellationToken cancellationToken)
    {
        var subDistricts =  await _context.SubDistricts
            .Include(c => c.District)
            .AsNoTracking()
            .Where(d => d.DistrictId==request.DistrictId)
            .OrderByDescending(x => x.Name) 
            .ToListAsync(cancellationToken);

        var subDistrictsList = _mapper.Map<List<SubDistrictViewModel>>(subDistricts);

        return new SubDistrictListViewModel()
        {
            SubDistricts = subDistrictsList
        };
    }
}
