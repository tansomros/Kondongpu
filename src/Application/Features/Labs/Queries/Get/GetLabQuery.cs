using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Labs.ViewModel;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Labs.Queries.Get;

public record GetLabQuery : IRequest<LabViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetLabQueryHandler : IRequestHandler<GetLabQuery, LabViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetLabQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LabViewModel> Handle(GetLabQuery request, CancellationToken cancellationToken)
    {
        var lab = await _context.Labs.FirstOrDefaultAsync(a => a.VisitNumber == request.VisitNumber, cancellationToken)
            ?? throw new NotFoundException(nameof(Lab), request.VisitNumber);

        return _mapper.Map<LabViewModel>(lab);
    }
}
