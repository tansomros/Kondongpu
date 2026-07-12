using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Dentals.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Dentals.Queries.Get;

public record GetDentalByIdQuery : IRequest<DentalViewModel>
{
    public required int Id { get; set; }
}

public class GetDentalByIdQueryHandler : IRequestHandler<GetDentalByIdQuery, DentalViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetDentalByIdQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DentalViewModel> Handle(GetDentalByIdQuery request, CancellationToken cancellationToken)
    {
        var dental = await _context.Dentals.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Dental), request.Id);

        return _mapper.Map<DentalViewModel>(dental);
    }
}
