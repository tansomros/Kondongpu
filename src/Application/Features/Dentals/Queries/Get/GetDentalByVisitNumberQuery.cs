using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Dentals.ViewModels;

namespace Kondongpu.Application.Features.Dentals.Queries.Get;

public class GetDentalByVisitNumberQuery : IRequest<DentalViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetDentalByVisitNumberQueryHandler : IRequestHandler<GetDentalByVisitNumberQuery, DentalViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetDentalByVisitNumberQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DentalViewModel> Handle(GetDentalByVisitNumberQuery request, CancellationToken cancellationToken)
    {
        var dental = await _context.Dentals
          .AsNoTracking()
          .FirstOrDefaultAsync(
              d => d.VisitNumber == request.VisitNumber
          , cancellationToken)
          ?? null;

        return _mapper.Map<DentalViewModel>(dental);
    }
}
