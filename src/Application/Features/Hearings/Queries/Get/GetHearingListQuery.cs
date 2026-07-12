using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Hearings.ViewModel;

namespace Kondongpu.Application.Features.Hearings.Queries.Get;

public record GetHearingListQuery : IRequest<PaginatedList<HearingViewModel>>
{    /// <summary>
     /// หน้าของข้อมูลเป็นเลขจำนวนเต็ม
     /// </summary>
    public int Page { get; init; } = 1;

    /// <summary>
    /// จำนวนของข้อมูลที่ต้องการเป็นเลขจำนวนเต็ม
    /// </summary>
    public int Length { get; init; } = 10;
}

public class GetHearingListQueryHandler : IRequestHandler<GetHearingListQuery, PaginatedList<HearingViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetHearingListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<HearingViewModel>> Handle(GetHearingListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Hearings
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .ProjectTo<HearingViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.Length, cancellationToken);
    }
}
