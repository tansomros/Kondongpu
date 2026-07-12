
using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;

namespace Kondongpu.Application.Features.ReportTemplates.Queries
{
    public class GetReportTemplateDetailQuery : IRequest<ReportTemplateDetailListViewModel>
    {
        public Int32 ReportTemplateId { get; set; }
    }
    internal class GetReportTemplateDetailQueryHandler : IRequestHandler<GetReportTemplateDetailQuery, ReportTemplateDetailListViewModel>
    {
        private readonly KondongpuDatabaseContext _context;
        private readonly IMapper _mapper;
        public GetReportTemplateDetailQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ReportTemplateDetailListViewModel> Handle(GetReportTemplateDetailQuery request, CancellationToken cancellationToken)
        {
            var report = await _context.ReportTemplateDetail.AsNoTracking()
                    .Where(r => r.ReportTemplateId.Equals(request.ReportTemplateId))            
                    .ToListAsync(cancellationToken);

    //        var rs = await _context.RoleReportTemplate
    //.AsNoTracking()
    //.ToListAsync(cancellationToken);

    //        rs = rs.Where(x => x.ReportTemplateList.Count > 0).ToList();

             
    //        var rolesId = rs.Where(x => x.ReportTemplateList.Contains(Convert.ToInt32(request.ReportTemplateId))).Select(r=>r.Id).ToList();

            //var xx= _mapper.Map<List<ReportTemplateDetailViewModel>>(reportDetails);

            //return new ReportTemplateDetailListViewModel() { Details = reportDetails,RolesId = rolesId};

            var reportList = _mapper.Map<List<ReportTemplateDetailViewModel>>(report);
            return new ReportTemplateDetailListViewModel()
            {
                Details = reportList,
            };

        }
    }
}
