using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.SpecialTests.ViewModel;

namespace Kondongpu.Application.Features.SpecialTests.Queries.Get;

public record GetSpecialTestListQuery : IRequest<PaginatedList<SpecialTestViewModel>>
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

public class GetSpecialTestListQueryHandler : IRequestHandler<GetSpecialTestListQuery, PaginatedList<SpecialTestViewModel>>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetSpecialTestListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SpecialTestViewModel>> Handle(GetSpecialTestListQuery request, CancellationToken cancellationToken)
    {
        return await _context.SpecialTests
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .ProjectTo<SpecialTestViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);
    }
}
