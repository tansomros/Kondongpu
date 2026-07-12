using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Labs.ViewModel;

namespace Kondongpu.Application.Features.Labs.Queries.Get;

public record GetLabListQuery : IRequest<PaginatedList<LabViewModel>>
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

public class GetLabListQueryHandler : IRequestHandler<GetLabListQuery, PaginatedList<LabViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetLabListQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LabViewModel>> Handle(GetLabListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Labs
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .ProjectTo<LabViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.Length, cancellationToken);

    }
}
