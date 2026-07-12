using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Patients.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Patients.Queries.Get;

public class GetPatientByHospitalNumberQuery : IRequest<PatientViewModel>
{
    public required string HospitalNumber { get; set; }
}

public class GetPatientByHospitalNumberQueryHandler : IRequestHandler<GetPatientByHospitalNumberQuery, PatientViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _checkupContext;
    public GetPatientByHospitalNumberQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
    {
        _checkupContext = checkupContext;
        _mapper = mapper;
    }

    public async Task<PatientViewModel> Handle(GetPatientByHospitalNumberQuery request, CancellationToken cancellationToken)
    {
        var patient = await _checkupContext
            .Patients
            .Include(p => p.Province)
            .Include(p => p.District)
            .Include(p => p.SubDistrict)
            .FirstOrDefaultAsync(p => p.HospitalNumber == request.HospitalNumber, cancellationToken)
            ?? throw new NotFoundException(nameof(Patient), request.HospitalNumber);
        return _mapper.Map<PatientViewModel>(patient);
       
    }
}
