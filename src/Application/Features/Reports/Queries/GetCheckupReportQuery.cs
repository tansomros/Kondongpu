using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Reports.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Reports.Queries;
public class GetCheckupReportQuery : IRequest<CheckupReportViewModel>
{
    public required int Id { get; set; }
}

public class GetCheckupReportQueryHandler : IRequestHandler<GetCheckupReportQuery, CheckupReportViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetCheckupReportQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CheckupReportViewModel> Handle(GetCheckupReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _context.Reports
                .Include(c => c.Patient).AsNoTracking()
                .Include(c => c.CheckupType)
                .Include(c => c.Checkup!).ThenInclude(c => c.Labs)
                    .ThenInclude(c => c.CheckupItem!).ThenInclude(c => c.CheckupGroup!).ThenInclude(c => c.CheckupClass!)
                    .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                 ?? throw new NotFoundException(nameof(Report), request.Id);
        return _mapper.Map<CheckupReportViewModel>(report);
    }
}
