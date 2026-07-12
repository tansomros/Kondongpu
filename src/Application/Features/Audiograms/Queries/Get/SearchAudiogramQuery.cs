using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Audiograms.ViewModel;

namespace Kondongpu.Application.Features.Audiograms.Queries.Get;

public record SearchAudiogramQuery : IRequest<PaginatedList<AudiogramViewModel>>
{
    /// <summary>
    /// คำค้น
    /// </summary>
    public required string KeyWord { get; set; }

    /// <summary>
    /// หน้าของข้อมูลเป็นเลขจำนวนเต็ม
    /// </summary>
    public required int Page { get; set; } = 1;

    /// <summary>
    /// จำนวนของข้อมูลที่ต้องการเป็นเลขจำนวนเต็ม
    /// </summary>
    public required int Length { get; set; } = 10;
}
public class SearchAudiogramQueryHandler : IRequestHandler<SearchAudiogramQuery, PaginatedList<AudiogramViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public SearchAudiogramQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AudiogramViewModel>> Handle(SearchAudiogramQuery request, CancellationToken cancellationToken)
    {
        return await _context.Audiograms
                .AsNoTracking()
                .Where(e => e.VisitNumber.Contains(request.KeyWord))
                .ProjectTo<AudiogramViewModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);
    }
}
