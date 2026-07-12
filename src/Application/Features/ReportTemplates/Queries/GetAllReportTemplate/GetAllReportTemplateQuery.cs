
using System.Collections.Generic;
using MediatR;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;

namespace Kondongpu.Application.Features.ReportTemplates.Queries
{
    public class GetAllReportTemplateQuery : IRequest<ReportTemplateListViewModel>
    {
    }
    public class GetAllReportTemplateHandler : IRequestHandler<GetAllReportTemplateQuery, ReportTemplateListViewModel>
    {
        private readonly KondongpuDatabaseContext _context;
        private readonly IMapper _mapper;

        public GetAllReportTemplateHandler(KondongpuDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReportTemplateListViewModel> Handle(GetAllReportTemplateQuery request, CancellationToken cancellationToken)
        {
            var report = await _context.ReportTemplate
                .Include(c => c.ReportGroup!).AsNoTracking()
                    .OrderBy(c => c.Name) 
                    .ToListAsync(cancellationToken);
            var reportList = _mapper.Map<List<ReportTemplateViewModel>>(report);

            return new ReportTemplateListViewModel()
            {
                ReportTemplates = reportList,
            };

        }
    }
}
