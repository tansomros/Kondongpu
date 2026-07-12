using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Visions.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Visions.Queries.Get;

public record GetVisionByIdQuery : IRequest<VisionViewModel>
{
    public required int Id { get; set; }
}

public class GetVisionByIdQueryHandler : IRequestHandler<GetVisionByIdQuery, VisionViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetVisionByIdQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VisionViewModel> Handle(GetVisionByIdQuery request, CancellationToken cancellationToken)
    {
        var vision = await _context.Visions.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Vision), request.Id);

        return _mapper.Map<VisionViewModel>(vision);
    }
}
