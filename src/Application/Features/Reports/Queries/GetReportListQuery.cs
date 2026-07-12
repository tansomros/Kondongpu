using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Reports.ViewModels;

namespace Kondongpu.Application.Features.Reports.Queries
{
    public class GetReportListQuery : IRequest<CheckupReportListViewModel>
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
    public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, CheckupReportListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _context;

        public GetReportListQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CheckupReportListViewModel> Handle(GetReportListQuery request, CancellationToken cancellationToken)
        {
            var reports = await _context
                .Reports.AsNoTracking()
                .Include(c => c.CheckupType)
                .Include(c => c.Patient)
                .Where(c => request.StartDate <= c.VisitDate && c.VisitDate <= request.EndDate)
                //.ProjectTo<CheckupViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var checkupList = _mapper.Map<List<CheckupReportViewModel>>(reports);

            return new CheckupReportListViewModel()
            {
                Reports = checkupList
            };
        }
    }
}
