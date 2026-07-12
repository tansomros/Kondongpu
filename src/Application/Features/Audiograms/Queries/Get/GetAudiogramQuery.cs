using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Audiograms.ViewModel;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Audiograms.Queries.Get;

public record GetAudiogramQuery : IRequest<AudiogramViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetAudiogramQueryHandler : IRequestHandler<GetAudiogramQuery, AudiogramViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetAudiogramQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AudiogramViewModel> Handle(GetAudiogramQuery request, CancellationToken cancellationToken)
    {
        var audiogram = await _context
            .Audiograms
            .Include(a => a.Hearings)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.VisitNumber == request.VisitNumber, cancellationToken)
            ?? throw new NotFoundException(nameof(Audiogram), request.VisitNumber);

        return _mapper.Map<AudiogramViewModel>(audiogram);
    }
}
