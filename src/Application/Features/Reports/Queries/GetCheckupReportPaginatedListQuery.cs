using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Reports.ViewModels;

namespace Kondongpu.Application.Features.Reports.Queries;
public class GetCheckupReportPaginatedListQuery 
    : IRequest<PaginatedList<CheckupReportViewModel>>
{
    public required string HospitalNumber { get; set; }
    public required int Page { get; set; }
    public required int Length { get; set; }
}

public class GetCheckupReportPaginatedListQueryHandler 
    : IRequestHandler<GetCheckupReportPaginatedListQuery, PaginatedList<CheckupReportViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _checkupContext;
    public GetCheckupReportPaginatedListQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
    {
        _checkupContext = checkupContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CheckupReportViewModel>> Handle(GetCheckupReportPaginatedListQuery request, CancellationToken cancellationToken)
    {
        var query = _checkupContext.Reports.AsQueryable();

        query = query
            .Where(r => r.HospitalNumber == request.HospitalNumber)
            .OrderByDescending(d => d.LastModified)
            .ThenBy(d => d.CreatedOn);

        var count = await query.CountAsync(cancellationToken);

        var filtered = await query
            .AsNoTracking()
            .Skip((request.Page - 1) * request.Length)
            .Take(request.Length)
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<List<CheckupReportViewModel>>(filtered);
        return new PaginatedList<CheckupReportViewModel>(viewModels, count, request.Page, request.Length);
    }
}
