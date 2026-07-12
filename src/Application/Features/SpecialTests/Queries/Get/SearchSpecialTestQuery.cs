using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.SpecialTests.ViewModel;

namespace Kondongpu.Application.Features.SpecialTests.Queries.Get;

public record SearchSpecialTestQuery : IRequest<PaginatedList<SpecialTestViewModel>>
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

public class SearchSpecialTestQueryHandler : IRequestHandler<SearchSpecialTestQuery, PaginatedList<SpecialTestViewModel>>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public SearchSpecialTestQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SpecialTestViewModel>> Handle(SearchSpecialTestQuery request, CancellationToken cancellationToken)
    {
        return await _context.SpecialTests
                .AsNoTracking()
                .Where(e => e.VisitNumber.Contains(request.KeyWord))
                .ProjectTo<SpecialTestViewModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);
    }
}
