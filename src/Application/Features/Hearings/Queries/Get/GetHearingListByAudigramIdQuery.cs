using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Hearings.ViewModel;

namespace Kondongpu.Application.Features.Hearings.Queries.Get;

public record GetHearingListByAudigramIdQuery : IRequest<PaginatedList<HearingViewModel>>
{
    /// <summary>
    /// คำค้น
    /// </summary>
    public required int AudiogramId { get; set; }

    /// <summary>
    /// หน้าของข้อมูลเป็นเลขจำนวนเต็ม
    /// </summary>
    public required int Page { get; set; } = 1;

    /// <summary>
    /// จำนวนของข้อมูลที่ต้องการเป็นเลขจำนวนเต็ม
    /// </summary>
    public required int Length { get; set; } = 10;
}

public class SearchHearingQueryHandler : IRequestHandler<GetHearingListByAudigramIdQuery, PaginatedList<HearingViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public SearchHearingQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<HearingViewModel>> Handle(GetHearingListByAudigramIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Hearings
                .AsNoTracking()
                .Where(e => e.AudiogramId == request.AudiogramId)
                .ProjectTo<HearingViewModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);
    }
}
