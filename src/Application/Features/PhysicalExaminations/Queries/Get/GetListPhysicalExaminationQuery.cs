using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.PhysicalExaminations.ViewModels;

namespace Kondongpu.Application.Features.PhysicalExaminations.Queries.Get;
public class GetListPhysicalExaminationQuery : IRequest<PhysicalExaminationListViewModel>
{

}

public class GetListPhysicalExaminationQueryHandler : IRequestHandler<GetListPhysicalExaminationQuery, PhysicalExaminationListViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetListPhysicalExaminationQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PhysicalExaminationListViewModel> Handle(GetListPhysicalExaminationQuery request, CancellationToken cancellationToken)
    {
        var physicalExamination = await _context.PhysicalExams.AsNoTracking().ToListAsync(cancellationToken);
        var physicalExaminationList = _mapper.Map<List<PhysicalExaminationListViewModel>>(physicalExamination);
        return new PhysicalExaminationListViewModel()
        {
            PhysicalExaminations = physicalExaminationList,
        };
    }
}
