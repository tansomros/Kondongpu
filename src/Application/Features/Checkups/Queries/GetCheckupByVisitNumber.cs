using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Queries;
public class GetCheckupByVisitNumberQuery : IRequest<CheckupViewModel>
{    
    public required string VisitNumber { get; set; }

    public class GetCheckupByVisitNumberQueryHandler : IRequestHandler<GetCheckupByVisitNumberQuery, CheckupViewModel>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _checkupContext;
        public GetCheckupByVisitNumberQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
        {
            _checkupContext = checkupContext;
            _mapper = mapper;
        }

        public async Task<CheckupViewModel> Handle(GetCheckupByVisitNumberQuery request, CancellationToken cancellationToken)
        {
            var checkup = await _checkupContext
                .Checkups
                .Include(c => c.Patient).AsNoTracking()
                .Include(c => c.CheckupType)
                .Include(c => c.Audiograms)
                .Include(c => c.BodyCompositions)
                .Include(c => c.Labs)
                    .ThenInclude(c => c.CheckupItem!).ThenInclude(c => c.CheckupGroup!).ThenInclude(c => c.CheckupClass!)
                    .AsNoTracking()
                .Include(c => c.Lungs)
                .Include(c => c.ConclusionBy)
                .Include(c => c.PhysicalExaminationBy)
                .Include(c => c.PhysicalExams)
                .Include(c => c.SpecialTests).ThenInclude(c => c.CheckupItem!).ThenInclude(c => c.CheckupGroup!).ThenInclude(c => c.CheckupClass!)
                    .AsNoTracking()
                .Include(c => c.Visions)
                .Include(c => c.Xrays)
                    .ThenInclude(c => c.CheckupItem!).ThenInclude(c => c.CheckupGroup!).ThenInclude(c => c.CheckupClass!)
                    .AsNoTracking()
                .FirstOrDefaultAsync(p => p.VisitNumber == request.VisitNumber, cancellationToken)
                ?? throw new NotFoundException(nameof(Checkup), request.VisitNumber);

            return _mapper.Map<CheckupViewModel>(checkup);
        }
    }
}


