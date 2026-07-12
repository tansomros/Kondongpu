using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Audiograms.ViewModel;

namespace Kondongpu.Application.Features.Audiograms.Queries.Get;

public record GetAudiogramListQuery : IRequest<PaginatedList<AudiogramViewModel>>
{
    /// <summary>
    /// หน้าของข้อมูลเป็นเลขจำนวนเต็ม
    /// </summary>
    public int Page { get; init; } = 1;

    /// <summary>
    /// จำนวนของข้อมูลที่ต้องการเป็นเลขจำนวนเต็ม
    /// </summary>
    public int Length { get; init; } = 10;
}

public class GetAudiogramListQueryHandler : IRequestHandler<GetAudiogramListQuery, PaginatedList<AudiogramViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetAudiogramListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AudiogramViewModel>> Handle(GetAudiogramListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Audiograms
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .ProjectTo<AudiogramViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);
    }
}
