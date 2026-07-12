using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Xrays.ViewModel;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Xrays.Queries.Get;

public record GetXrayQuery : IRequest<XrayViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetXrayQueryHandler : IRequestHandler<GetXrayQuery, XrayViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetXrayQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<XrayViewModel> Handle(GetXrayQuery request, CancellationToken cancellationToken)
    {

        var specialTest = await _context.Xrays
            .AsNoTracking()
            .Include(c => c.CheckupItem)
            .ThenInclude(item => item.CheckupGroup)
            .ThenInclude(group => group.CheckupClass)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.VisitNumber == request.VisitNumber, cancellationToken)
            ?? throw new NotFoundException(nameof(Xray), request.VisitNumber);


        var model = _mapper.Map<XrayViewModel>(specialTest);
        return model;
    }
}
