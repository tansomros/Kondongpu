using Kondongpu.Application.Common.Mappings;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Patients.ViewModels;
using MediatR;
using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Patients.Queries.Get;

public class GetPatientListQuery : IRequest<PaginatedList<PatientViewModel>>
{
    /// <summary>
    /// หน้าของข้อมูลเป็นเลขจำนวนเต็ม
    /// </summary>
    public int Page { get; init; } = 1;

    /// <summary>
    /// จำนวนของข้อมูลที่ต้องการเป็นเลขจำนวนเต็ม
    /// </summary>
    public int Limit { get; init; } = 10;
}
public class GetPatientListQueryHandler : IRequestHandler<GetPatientListQuery, PaginatedList<PatientViewModel>>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetPatientListQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<PatientViewModel>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .ProjectTo<PatientViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.Limit);
    }
}
