using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Commands.Update;

public record UpdateCheckupCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public int CheckupTypeId { get; set; }
    public int Status { get; set; }
    public DateTime? SaveDate { get; set; }
    public bool IsFinalized { get; set; }
    public DateTime? FinalizedDate { get; set; }
    public int PhysicalExaminationById { get; set; }
    public string? Conclusion { get; set; }
    public int ConclusionById { get; set; }

    public bool? IsSmoking { get; set; }
    public string? Smoking { get; set; }
    public bool? IsAlcohol { get; set; }
    public string? Alcohol { get; set; }
    //public bool IsLabResultReady { get; set; }
    //public bool IsXrayResultReady { get; set; }
    //public double Weight { get; set; }
    //public double Height { get; set; }
    //public double Temperature { get; set; }
    //public int? PulseRate { get; set; }
    //public int? SystolicBloodPresure { get; set; }
    //public int? DiastolicBloodPresure { get; set; }
    //public int? RespiratoryRate { get; set; }
    //public string? PhysicalExaminationConclusion { get; set; }
    //public string? LabResultConclusion { get; set; }
    //public string? XrayResultConclusion { get; set; }
    //public string? SpecialConclusion { get; set; }
    //public bool IsMain { get; set; } = true;

    //public required List<string> LabOrder { get; set; }
    //public required List<string> XrayOrder { get; set; }
    //public required List<string> ServiceOrder { get; set; }
}

public class UpdateCheckupCommandHandler : IRequestHandler<UpdateCheckupCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;
    private readonly IDateTime _dateTime;

    public UpdateCheckupCommandHandler(
        KondongpuDatabaseContext context, 
        IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<Unit> Handle(UpdateCheckupCommand request, CancellationToken cancellationToken)
    {
        var checkup = await _context
            .Checkups
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (checkup == null)
        {
            throw new NotFoundException(nameof(Checkup), request.Id);
        }
        checkup.CheckupTypeId = request.CheckupTypeId;
        checkup.SaveDate = _dateTime.Now.DateTime.ToUniversalTime();
        checkup.Status = request.Status;
        checkup.IsFinalized = request.IsFinalized;
        checkup.FinalizedDate = request.FinalizedDate;
        checkup.PhysicalExaminationById = request.PhysicalExaminationById;
        checkup.Conclusion = request.Conclusion;
        checkup.ConclusionById = request.ConclusionById;
        checkup.IsSmoking = request.IsSmoking;
        checkup.SmokingRemark = request.Smoking;
        checkup.IsAlcohol = request.IsAlcohol;
        checkup.AlcoholRemark = request.Alcohol;
        //checkup.IsLabResultReady = request.IsLabResultReady;
        //checkup.IsXrayResultReady = request.IsXrayResultReady;
        //checkup.Weight = request.Weight;
        //checkup.Height = request.Height;
        //checkup.Temperature = request.Temperature;
        //checkup.PulseRate = request.PulseRate;
        //checkup.SystolicBloodPresure = request.SystolicBloodPresure;
        //checkup.DiastolicBloodPresure = request.DiastolicBloodPresure;
        //checkup.RespiratoryRate = request.RespiratoryRate;
        //checkup.PhysicalExaminationConclusion = request.PhysicalExaminationConclusion;
        //checkup.LabResultConclusion = request.LabResultConclusion;
        //checkup.XrayResultConclusion = request.XrayResultConclusion;
        //checkup.SpecialConclusion = request.SpecialConclusion;
        //checkup.IsMain = request.IsMain;
        //checkup.LabOrder = request.LabOrder;
        //checkup.XrayOrder = request.XrayOrder;
        //checkup.ServiceOrder = request.ServiceOrder;

        _context.Checkups.Update(checkup);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
