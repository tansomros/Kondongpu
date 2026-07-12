using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Visions.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Visions.Queries.Get;
public record GetVisionByVNQuery : IRequest<VisionViewModel>
{
    public required int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
}

public class GetVisionByVNQueryHandler : IRequestHandler<GetVisionByVNQuery, VisionViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetVisionByVNQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VisionViewModel> Handle(GetVisionByVNQuery request, CancellationToken cancellationToken)
    {
        var vision = await _context.Visions
            .AsNoTracking()
            .FirstOrDefaultAsync(
                d => d.VisitNumber == request.VisitNumber 
                && d.CheckupId == request.CheckupId
            , cancellationToken)
            ?? throw new NotFoundException(nameof(Vision), request.VisitNumber);

        return _mapper.Map<VisionViewModel>(vision);
    }
}
