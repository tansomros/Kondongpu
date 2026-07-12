using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Patients.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Patients.Queries.Get;

public class GetPatientQuery : IRequest<PatientViewModel>
{
    public int Id { get; set; }
}

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PatientViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _checkupContext;
    public GetPatientQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
    {
        _checkupContext = checkupContext;
        _mapper = mapper;
    }

    public async Task<PatientViewModel> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _checkupContext.Patients.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Patient), request.Id);
        return _mapper.Map<PatientViewModel>(patient);
       
    }
}
