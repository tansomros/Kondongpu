using System.Runtime.CompilerServices;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;
using Kondongpu.Domain.ValueObjects;
using CC = Kondongpu.Domain.ValueObjects.CheckupClassValue;
using CG = Kondongpu.Domain.ValueObjects.CheckupGroupValue;


namespace Kondongpu.Application.Features.Reports.Commands;

public record UpdateReportCommand : IRequest<Unit>
{
    //public int Id { get; set; }
#pragma warning disable CS8618
    //public required int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    //public required int CheckupItemId { get; set; }
    //public required int PatientId { get; set; }
    //public required int CheckupVisitId { get; set; }
    //public required string HospitalNumber { get; set; }
    //public required DateOnly VisitDate { get; set; }
    //public required string VisitTime { get; set; }
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

    //public int? AgeCheckup { get; set; }
    //public string? AgeTextCheckup { get; set; }
    public string? PhysicalExaminationBy { get; set; }
    public string? ConclusionBy { get; set; }
    public string? Conclusion { get; set; }

    public string? SpecialConclusion { get; set; }
    public string? BmiReport { get; set; }
    public string? BpReport { get; set; }
    public string? AudiogramReport { get; set; }  
    public string? DentalReport { get; set; }
    public string? HbReport { get; set; }
    public string? WbcReport { get; set; }
    public string? PlateletReport { get; set; }
    public string? CbcReport { get; set; }
    public string? FbsReport { get; set; }
    public string? LipidReport { get; set; }
    public string? UricReport { get; set; }
    public string? RenalReport { get; set; }
    public string? LiverReport { get; set; }
    public string? HepatitisReport { get; set; }
    public string? ThyroidReport { get; set; }
    public string? ImmunologyReport { get; set; }
    public string? UrineReport { get; set; }
    public string? StoolReport { get; set; }
    public string? VisionReport { get; set; }
    public string? LungReport { get; set; }
#pragma warning restore CS8618

}

public class UpdateCommandHandler : IRequestHandler<UpdateReportCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
       
        var report = await _context.Reports.FirstOrDefaultAsync(b => b.VisitNumber.Equals(request.VisitNumber), cancellationToken);

        if (report == null)
        {
            throw new NotFoundException(nameof(report), request.VisitNumber);
        }
        report.CheckupTypeId = request.CheckupTypeId;

        report.PayorName = request.PayorName;
        report.PackageId = request.PackageId;
        report.PackageName = request.PackageName;
        report.Weight = request.Weight;
        report.Height = request.Height;
        report.Temperature = request.Temperature;
        report.DiastolicBloodPresure = request.DiastolicBloodPresure;
        report.SystolicBloodPresure = request.SystolicBloodPresure;
        report.PulseRate = request.PulseRate;
        report.RespiratoryRate = request.RespiratoryRate;

        report.PhysicalExaminationBy = request.PhysicalExaminationBy;
        report.ConclusionBy = request.ConclusionBy;
        report.Conclusion = request.Conclusion;
        report.SpecialConclusion = request.SpecialConclusion; 
        report.BmiReport = request.BmiReport;
        report.BpReport = request.BpReport;
        report.VisionReport = request.VisionReport;
        report.AudiogramReport = request.AudiogramReport;
        report.DentalReport = request.DentalReport;
        report.LungReport = request.LungReport; 
        report.HbReport = request.HbReport;
        report.WbcReport = request.WbcReport;
        report.PlateletReport = request.PlateletReport;
        report.CbcReport = request.CbcReport;
        report.FbsReport = request.FbsReport;
        report.LipidReport = request.LipidReport;
        report.UricReport = request.UricReport;
        report.RenalReport = request.RenalReport;
        report.LiverReport = request.LiverReport;
        report.HepatitisReport = request.HepatitisReport;
        report.ThyroidReport = request.ThyroidReport;
        report.ImmunologyReport = request.ImmunologyReport;
        report.UrineReport = request.UrineReport;
        report.StoolReport = request.StoolReport;

        #region "SaveLab"
        var labs = await _context.Labs
            .Include(c => c.CheckupItem!).ThenInclude(c => c.CheckupGroup!).ThenInclude(c => c.CheckupClass!)
            .Where(l => l.VisitNumber == request.VisitNumber)
            .ToListAsync(cancellationToken);

        report.Wbc = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.WhiteBloodCell)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };

        report.Fbs = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.BloodSugar)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };

        report.Lipid = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.Lipid)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };

        report.Renal = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.Renal)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };

        report.Liver = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.Liver)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };

        report.Urine = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.Urine && s.CheckupItem?.IsDisplayPrint == true)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };

        report.Stool = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.StoolExam)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };

        report.OtherLab = new LabReport()
        {
            Labs = labs
            .Where(s => s.CheckupItem?.CheckupGroup?.CheckupClass.Code == CC.SpecialTest)
            .Select(s => new LabStructure()
            {
                CheckupItemId = s.CheckupItemId ?? 0,
                CheckupItemCode = s.CheckupItem?.Code ?? string.Empty,
                CheckupGroupCode = s.CheckupItem?.CheckupGroup?.Code ?? string.Empty,
                Name = s.CheckupItem?.DisplayName ?? string.Empty,
                ReferenceRange = s.ReferenceRange ?? string.Empty,
                IsAbnormal = s.IsAbnormal,
                ResultValue = s.ResultValue,
            })
            .ToList()
        };
        #endregion


        _context.Reports.Update(report);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

