using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Labs.ViewModel;

namespace Kondongpu.Application.Features.Labs.Queries.Get;

public record SearchLabQuery : IRequest<PaginatedList<LabViewModel>>
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

public class SearchLabQueryHandler : IRequestHandler<SearchLabQuery, PaginatedList<LabViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;


    public SearchLabQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LabViewModel>> Handle(SearchLabQuery request, CancellationToken cancellationToken)
    {
        return await _context.Labs
                .AsNoTracking()
                .Where(e => e.VisitNumber.Contains(request.KeyWord))
                .ProjectTo<LabViewModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Length, cancellationToken: cancellationToken);

    }
}
