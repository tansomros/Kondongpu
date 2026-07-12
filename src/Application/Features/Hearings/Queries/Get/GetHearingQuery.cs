using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Hearings.ViewModel;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Hearings.Queries.Get;

public record GetHearingQuery : IRequest<HearingViewModel>
{
    public required int Id { get; set; }
}

public class GetHearingQueryHandler : IRequestHandler<GetHearingQuery, HearingViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetHearingQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HearingViewModel> Handle(GetHearingQuery request, CancellationToken cancellationToken)
    {
        var hearing = await _context.Hearings.AsNoTracking().FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Hearing), request.Id);

        return _mapper.Map<HearingViewModel>(hearing);
    }
}
