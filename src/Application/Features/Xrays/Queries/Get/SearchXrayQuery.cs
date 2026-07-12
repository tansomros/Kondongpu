using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Xrays.ViewModel;

namespace Kondongpu.Application.Features.Xrays.Queries.Get;

public record SearchXrayQuery : IRequest<PaginatedList<XrayViewModel>>
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

public class SearchXrayQueryHandler : IRequestHandler<SearchXrayQuery, PaginatedList<XrayViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public SearchXrayQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<XrayViewModel>> Handle(SearchXrayQuery request, CancellationToken cancellationToken)
    {
        return await _context.Xrays
                .AsNoTracking()
                .Where(e => e.VisitNumber.Contains(request.KeyWord))
                .ProjectTo<XrayViewModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);
    }
}
