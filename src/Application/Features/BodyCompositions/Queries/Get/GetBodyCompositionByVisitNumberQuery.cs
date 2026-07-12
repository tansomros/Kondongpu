using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.BodyCompositions.ViewModels;

namespace Kondongpu.Application.Features.BodyCompositions.Queries.Get;

public class GetBodyCompositionByVisitNumberQuery : IRequest<BodyCompositionViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetBodyCompositionByVisitNumberQueryHandler : IRequestHandler<GetBodyCompositionByVisitNumberQuery, BodyCompositionViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetBodyCompositionByVisitNumberQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BodyCompositionViewModel> Handle(GetBodyCompositionByVisitNumberQuery request, CancellationToken cancellationToken)
    {
        var bodyComposition = await _context.BodyCompositions
          .AsNoTracking()
          .FirstOrDefaultAsync(
              d => d.VisitNumber == request.VisitNumber
          , cancellationToken)
          ?? null;

        return _mapper.Map<BodyCompositionViewModel>(bodyComposition);
    }
}
