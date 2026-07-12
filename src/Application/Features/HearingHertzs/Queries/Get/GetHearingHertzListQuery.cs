using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.HearingHertzs.ViewModel;

namespace Kondongpu.Application.Features.HearingHertzs.Queries.Get;

public record GetHearingHertzListQuery : IRequest<PaginatedList<HearingHertzViewModel>>
{    /// <summary>
     /// หน้าของข้อมูลเป็นเลขจำนวนเต็ม
     /// </summary>
    public int Page { get; init; } = 1;

    /// <summary>
    /// จำนวนของข้อมูลที่ต้องการเป็นเลขจำนวนเต็ม
    /// </summary>
    public int Length { get; init; } = 10;
}

public class GetHearingHertzListQueryHandler : IRequestHandler<GetHearingHertzListQuery, PaginatedList<HearingHertzViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetHearingHertzListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<HearingHertzViewModel>> Handle(GetHearingHertzListQuery request, CancellationToken cancellationToken)
    {
        return await _context.HearingHertzs
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .ProjectTo<HearingHertzViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.Length, cancellationToken);
    }
}
