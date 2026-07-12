using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Reports.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Reports.Queries;

public class GetPatientReportByHospitalNumberQuery : IRequest<PatientReportViewModel>
{
    public required string HospitalNumber { get; set; }
}

public class PatientReportByHospitalNumberQueryHandler : IRequestHandler<GetPatientReportByHospitalNumberQuery, PatientReportViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _checkupContext;
    public PatientReportByHospitalNumberQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
    {
        _checkupContext = checkupContext;
        _mapper = mapper;
    }

    public async Task<PatientReportViewModel> Handle(GetPatientReportByHospitalNumberQuery request, CancellationToken cancellationToken)
    {
        var patient = await _checkupContext.Patients.FirstOrDefaultAsync(p => p.HospitalNumber == request.HospitalNumber, cancellationToken)
            ?? throw new NotFoundException(nameof(Patient), request.HospitalNumber);
        return _mapper.Map<PatientReportViewModel>(patient);
       
    }
}
