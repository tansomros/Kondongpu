using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.PhysicalExaminations.ViewModels;

namespace Kondongpu.Application.Features.PhysicalExaminations.Queries.Get;

public record GetByIdPhysicalExaminationQuery : IRequest<PhysicalExaminationListViewModel>
{
    public required int Id { get; set; }
}

public class GetByIdPhysicalExaminationQueryHandler : IRequestHandler<GetByIdPhysicalExaminationQuery, PhysicalExaminationListViewModel>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetByIdPhysicalExaminationQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PhysicalExaminationListViewModel> Handle(GetByIdPhysicalExaminationQuery request, CancellationToken cancellationToken)
    {
        var physicalExamination = await _context.PhysicalExams.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(PhysicalExaminations), request.Id);

        return _mapper.Map<PhysicalExaminationListViewModel>(physicalExamination);
    }
}
