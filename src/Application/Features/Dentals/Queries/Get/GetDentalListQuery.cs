using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Dentals.ViewModels;

namespace Kondongpu.Application.Features.Dentals.Queries.Get;

public class GetDentalListQuery : IRequest<DentalListViewModel>
{

}

public class GetDentalListQueryHandler : IRequestHandler<GetDentalListQuery, DentalListViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetDentalListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DentalListViewModel> Handle(GetDentalListQuery request, CancellationToken cancellationToken)
    {
        var dentals = await _context.Dentals.AsNoTracking().ToListAsync(cancellationToken);
        var dentalList = _mapper.Map<List<DentalViewModel>>(dentals);
        return new DentalListViewModel()
        {
            Dentals = dentalList,
        };
    }
}
