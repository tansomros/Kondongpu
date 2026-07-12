using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.CheckupItems.Queries.Get;
using Kondongpu.Application.Features.Lungs.ViewModels;

namespace Kondongpu.Application.Features.Lungs.Queries.Get;

public class GetLungByVisitNumberQuery : IRequest<LungViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetLungByVisitNumberQueryHandler : IRequestHandler<GetLungByVisitNumberQuery, LungViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetLungByVisitNumberQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LungViewModel> Handle(GetLungByVisitNumberQuery request, CancellationToken cancellationToken)
    {  
        var lung = await _context.Lungs
          .AsNoTracking()
          .FirstOrDefaultAsync(
              d => d.VisitNumber == request.VisitNumber            
          , cancellationToken)
          ?? null;

        return _mapper.Map<LungViewModel>(lung); 
    }
}
