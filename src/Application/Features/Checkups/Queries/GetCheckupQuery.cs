using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Queries
{
    public class GetCheckupQuery : IRequest<CheckupViewModel>
    {
        public int Id { get; set; }
    }

    public class GetVisitQueryHandler : IRequestHandler<GetCheckupQuery, CheckupViewModel>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _checkupContext;
        public GetVisitQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
        {
            _checkupContext = checkupContext;
            _mapper = mapper;
        }

        public async Task<CheckupViewModel> Handle(GetCheckupQuery request, CancellationToken cancellationToken)
        {
            var checkup = await _checkupContext
                .Checkups
                .Include(c => c.Patient)
                .AsNoTracking()
                .Include(c => c.CheckupType)
                .Include(c => c.Audiograms)
                .Include(c => c.Labs)
                .Include(c => c.Lungs)
                .Include(c => c.PhysicalExams)
                .Include(c => c.SpecialTests)
                .Include(c => c.Visions)
                .Include(c => c.Xrays)
                .Include(c => c.ConclusionBy)
                .Include(c => c.PhysicalExaminationBy)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Checkup), request.Id);

            return _mapper.Map<CheckupViewModel>(checkup);
        }
    }
}
