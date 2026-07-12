using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Lungs.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Lungs.Queries.Get;

public record GetLungByIdQuery : IRequest<LungViewModel>
{
    public required int Id { get; set; }
}

public class GetLungByIdQueryHandler : IRequestHandler<GetLungByIdQuery, LungViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetLungByIdQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LungViewModel> Handle(GetLungByIdQuery request, CancellationToken cancellationToken)
    {
        var lung = await _context.Lungs.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Lung), request.Id);

        return _mapper.Map<LungViewModel>(lung);
    }
}
