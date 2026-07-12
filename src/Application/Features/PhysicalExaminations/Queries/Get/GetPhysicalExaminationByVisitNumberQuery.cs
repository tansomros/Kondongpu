using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.CheckupItems.Queries.Get;
using Kondongpu.Application.Features.PhysicalExaminations.ViewModels;

namespace Kondongpu.Application.Features.PhysicalExaminations.Queries.Get;

public class GetPhysicalExaminationByVisitNumberQuery : IRequest<PhysicalExaminationViewModel>
{
    public required string VisitNumber { get; set; }
}

public class GetPhysicalExaminationByVisitNumberQueryHandler : IRequestHandler<GetPhysicalExaminationByVisitNumberQuery, PhysicalExaminationViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetPhysicalExaminationByVisitNumberQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PhysicalExaminationViewModel> Handle(GetPhysicalExaminationByVisitNumberQuery request, CancellationToken cancellationToken)
    {  
        var physicalExamination = await _context.PhysicalExams
          .AsNoTracking()
          .FirstOrDefaultAsync(
              d => d.VisitNumber == request.VisitNumber            
          , cancellationToken)
          ?? null;

        return _mapper.Map<PhysicalExaminationViewModel>(physicalExamination); 
    }
}
