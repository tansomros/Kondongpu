using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.ViewModels;

namespace Kondongpu.Application.Features.Checkups.Queries
{
    public class GetCheckupListQuery : IRequest<CheckupListViewModel>
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
    public class GetCheckupListQueryHandler : IRequestHandler<GetCheckupListQuery, CheckupListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _context;

        public GetCheckupListQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CheckupListViewModel> Handle(GetCheckupListQuery request, CancellationToken cancellationToken)
        {
            var checkups = await _context
                .Checkups.AsNoTracking()
                .Include(c => c.CheckupType)
                .Include(c => c.Patient)
                .Where(c => request.StartDate <= c.VisitDate && c.VisitDate <= request.EndDate)
                //.ProjectTo<CheckupViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var checkupList = _mapper.Map<List<CheckupViewModel>>(checkups);

            return new CheckupListViewModel()
            {
                Checkups = checkupList
            };
        }
    }
}
