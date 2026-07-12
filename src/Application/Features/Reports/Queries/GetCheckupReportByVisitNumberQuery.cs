using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Reports.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Reports.Queries;

/// <summary>
/// สำหรับรายงานรายบุคคล
/// </summary>
public class GetCheckupReportByVisitNumberQuery : IRequest<CheckupReportViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetCheckupReportByVisitNumberQueryHandler : IRequestHandler<GetCheckupReportByVisitNumberQuery, CheckupReportViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _checkupContext;
    public GetCheckupReportByVisitNumberQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
    {
        _checkupContext = checkupContext;
        _mapper = mapper;
    }

    public async Task<CheckupReportViewModel> Handle(GetCheckupReportByVisitNumberQuery request, CancellationToken cancellationToken)
    {
        var report = await _checkupContext.Reports
                .Include(c => c.Patient!).ThenInclude(c => c.Province!)
                .Include(c => c.Patient!).ThenInclude(c => c.District!)
                .Include(c => c.Patient!).ThenInclude(c => c.SubDistrict!)
                .Include(c => c.CheckupType)
                .Include(c => c.Checkup!).ThenInclude(c=> c.CheckupType!)
                .Include(c => c.Checkup!).ThenInclude(c => c.Patient!)
                .Include(c => c.Checkup!).ThenInclude(c => c.Visions!)
                .Include(c => c.Checkup!).ThenInclude(c => c.Audiograms)
                .Include(c => c.Checkup!).ThenInclude(c => c.Lungs!)                
                .Include(c => c.Checkup!).ThenInclude(c => c.ConclusionBy)
                .Include(c => c.Checkup!).ThenInclude(c => c.PhysicalExaminationBy)
                .Include(c => c.Checkup!).ThenInclude(c => c.PhysicalExams)
                .Include(c => c.Checkup!).ThenInclude(c => c.SpecialTests)
                .Include(c => c.Checkup!).ThenInclude(c => c.Xrays).ThenInclude(c => c.CheckupItem!).ThenInclude(c => c.CheckupGroup!).ThenInclude(c => c.CheckupClass!).AsNoTracking()
                .Include(c => c.Checkup!).ThenInclude(c => c.Labs).ThenInclude(c => c.CheckupItem!).ThenInclude(c => c.CheckupGroup!).ThenInclude(c => c.CheckupClass!).AsNoTracking()
               
                .FirstOrDefaultAsync(p => p.VisitNumber == request.VisitNumber, cancellationToken);
        return _mapper.Map<CheckupReportViewModel>(report);       
    }
}
