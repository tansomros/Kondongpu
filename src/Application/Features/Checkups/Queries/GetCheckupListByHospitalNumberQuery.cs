using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.ViewModels;

namespace Kondongpu.Application.Features.Checkups.Queries
{
    public class GetCheckupListByHospitalNumberQuery : IRequest<CheckupListViewModel>
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public required string HospitalNumber { get; set; }
    }
    public class GetCheckupListByHospitalNumberQueryHandler : IRequestHandler<GetCheckupListByHospitalNumberQuery, CheckupListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _context;

        public GetCheckupListByHospitalNumberQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CheckupListViewModel> Handle(GetCheckupListByHospitalNumberQuery request, CancellationToken cancellationToken)
        {
            var checkups = await _context
                .Checkups.AsNoTracking()
                .Include(c => c.CheckupType)
                .Include(c => c.Patient)
                .Where(c => c.HospitalNumber == request.HospitalNumber && (request.StartDate <= c.VisitDate && c.VisitDate <= request.EndDate))
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
