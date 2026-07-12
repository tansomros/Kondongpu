using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.SpecialTests.ViewModel;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.SpecialTests.Queries.Get;

public record GetSpecialTestQuery : IRequest<SpecialTestViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetSpecialTestQueryHandler : IRequestHandler<GetSpecialTestQuery, SpecialTestViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetSpecialTestQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SpecialTestViewModel> Handle(GetSpecialTestQuery request, CancellationToken cancellationToken)
    {
        var specialTest = await _context.SpecialTests
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.VisitNumber == request.VisitNumber, cancellationToken)
            ?? throw new NotFoundException(nameof(SpecialTest), request.VisitNumber);

        var model = _mapper.Map<SpecialTestViewModel>(specialTest);
        return model;
    }
}
