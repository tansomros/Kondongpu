using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.BodyCompositions.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.BodyCompositions.Queries.Get;

public record GetBodyCompositionByIdQuery : IRequest<BodyCompositionViewModel>
{
    public required int Id { get; set; }
}

public class GetBodyCompositionByIdQueryHandler : IRequestHandler<GetBodyCompositionByIdQuery, BodyCompositionViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetBodyCompositionByIdQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BodyCompositionViewModel> Handle(GetBodyCompositionByIdQuery request, CancellationToken cancellationToken)
    {
        var bodyComposition = await _context.BodyCompositions.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(BodyComposition), request.Id);

        return _mapper.Map<BodyCompositionViewModel>(bodyComposition);
    }
}
