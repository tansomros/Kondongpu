using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;

namespace Kondongpu.Application.Features.ReportTemplates.Queries
{
    public class GetReportGroupHandler : IRequestHandler<GetReportGroupQuery, List<ReportGroupViewModel>>
    {
        private readonly KondongpuDatabaseContext _context;
        private readonly IMapper _mapper;
        public GetReportGroupHandler(KondongpuDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ReportGroupViewModel>> Handle(GetReportGroupQuery request, CancellationToken cancellationToken)
        {
            return await _context.ReportGroup
                .ProjectTo<ReportGroupViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
