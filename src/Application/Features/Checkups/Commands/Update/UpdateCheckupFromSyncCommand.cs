using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Commands.Update;

public record UpdateCheckupFromSyncCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required int CheckupVisitId { get; set; }
    public required string VisitNumber { get; set; }
    public required string HospitalNumber { get; set; }

    //public  DateOnly VisitDate { get; set; }
    //public  TimeOnly VisitTime { get; set; }

    public required int CheckupTypeId { get; set; }
    public required string PayorName { get; set; }
    public required int PackageId { get; set; }
    public required string PackageName { get; set; }

    public double Weight { get; set; }
    public double Height { get; set; }
    public double Temperature { get; set; }
    public int? DiastolicBloodPresure { get; set; }
    public int? SystolicBloodPresure { get; set; }
    public int? PulseRate { get; set; }
    public int? RespiratoryRate { get; set; }

    public int? Age { get; set; }
    public string? AgeText { get; set; }
    public bool IsLabResultReady { get; set; }
    public bool IsXrayResultReady { get; set; }


    public required List<string> LabOrder { get; set; }
    public required List<string> XrayOrder { get; set; }
    public required List<string> ServiceOrder { get; set; }

    public required double Waist { get; set; }
    public bool? IsSmoking { get; set; }
    public bool? IsAlcohol { get; set; }
    public string? SmokingRemark { get; set; }
    public string? AlcoholRemark { get; set; }
}

public class UpdateCheckupFromSyncCommandHandler : IRequestHandler<UpdateCheckupFromSyncCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCheckupFromSyncCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCheckupFromSyncCommand request, CancellationToken cancellationToken)
    {
        var checkupItemCode = await _context.CheckupItems
            .Select(s => s.LabItemCode)
            .ToListAsync(cancellationToken);

        var checkup = await _context
            .Checkups
            .FirstOrDefaultAsync(
            c => c.Id == request.Id 
            && c.CheckupVisitId == request.CheckupVisitId
            && c.VisitNumber == request.VisitNumber
            && c.HospitalNumber == request.HospitalNumber
            , cancellationToken
            ) ?? throw new NotFoundException(nameof(Checkup), request.Id);

        //checkup.VisitDate = request.VisitDate;
        //checkup.VisitTime = request.VisitTime;

        checkup.CheckupTypeId = request.CheckupTypeId;
        checkup.PayorName = request.PayorName;
        checkup.PackageId = request.PackageId   ;
        checkup.PackageName = request.PackageName;

        checkup.Weight = request.Weight ;
        checkup.Height = request.Height;
        checkup.Temperature = request.Temperature;
        checkup.DiastolicBloodPresure = request.DiastolicBloodPresure;
        checkup.SystolicBloodPresure = request.SystolicBloodPresure;
        checkup.PulseRate = request.PulseRate;
        checkup.RespiratoryRate = request.RespiratoryRate;
        checkup.AgeCheckup = request.Age;
        checkup.AgeTextCheckup = request.AgeText;
        checkup.IsLabResultReady = request.IsLabResultReady;
        checkup.IsXrayResultReady = request.IsXrayResultReady;

        //checkup.LabOrder = request.LabOrder.Where(checkupItemCode.Contains).ToList(); ;
        //checkup.XrayOrder = request.XrayOrder.Where(checkupItemCode.Contains).ToList(); ;
        //checkup.ServiceOrder = request.ServiceOrder.Where(checkupItemCode.Contains).ToList(); 

        checkup.Waist = request.Waist;
        checkup.IsSmoking = request.IsSmoking;
        checkup.IsAlcohol = request.IsAlcohol;  
        checkup.SmokingRemark = request.SmokingRemark;
        checkup.AlcoholRemark = request.AlcoholRemark;

        _context.Checkups.Update(checkup);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}
