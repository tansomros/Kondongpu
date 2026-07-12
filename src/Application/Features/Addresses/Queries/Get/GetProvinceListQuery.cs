using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Addresses.ViewModel;

namespace Kondongpu.Application.Features.Addresses.Queries.Get;

public record GetProvinceListQuery : IRequest<ProvinceListViewModel>
{
  
}

public class GetProvinceListQueryHandler : IRequestHandler<GetProvinceListQuery,ProvinceListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetProvinceListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProvinceListViewModel> Handle(GetProvinceListQuery request, CancellationToken cancellationToken)
    {
        var provinces =  await _context.Provinces
            .AsNoTracking()
            .OrderByDescending(x => x.Name) 
            .ToListAsync(cancellationToken);

        var provincesList = _mapper.Map<List<ProvinceViewModel>>(provinces);

        return new ProvinceListViewModel()
        {
            Provinces = provincesList
        };
    }
}
