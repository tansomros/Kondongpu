using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;
using Kondongpu.Domain.ValueObjects;
using CC = Kondongpu.Domain.ValueObjects.CheckupClassValue;
using CG = Kondongpu.Domain.ValueObjects.CheckupGroupValue;

namespace Kondongpu.Application.Features.Reports.Commands;

public record CreateReportCommand : IRequest<int>
{
    public required int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required int CheckupItemId { get; set; }
    public required int PatientId { get; set; }
    public required int CheckupVisitId { get; set; } 
    public required string HospitalNumber { get; set; }
    public required DateOnly VisitDate { get; set; }
    public required string VisitTime { get; set; }
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

    public int? AgeCheckup { get; set; }
    public string? AgeTextCheckup { get; set; }

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
}

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    private int? _checkupId;
    private int? _checkupItemId; 
    private readonly KondongpuDatabaseContext _context;
    public CreateReportCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.CheckupId)
            .NotEmpty().WithMessage("Checkup Id ต้องไม่เป็นค่าว่าง")
            .NotNull().WithMessage("Checkup Id ต้องไม่เป็นค่า NULL")
            .MustAsync(CheckupExistAsync).WithMessage($"ไม่พบรายการ Checkup หมายเลข {_checkupId} ที่ท่านระบุ");     

        RuleFor(p => p.VisitNumber).NotEmpty().WithMessage("VisitNumber เป็นค่าว่างไม่ได้");

      
    }


    public async Task<bool> CheckupItemExistAsync(int checkupItemId, CancellationToken cancellationToken)
    {
        _checkupItemId = checkupItemId;
        return await _context.CheckupItems.AsNoTracking().AnyAsync(p => p.Id == checkupItemId, cancellationToken);
    }

    public async Task<bool> CheckupExistAsync(int checkupId, CancellationToken cancellationToken)
    {
        _checkupId = checkupId;
        return await _context.Checkups.AsNoTracking().AnyAsync(c => c.Id == checkupId, cancellationToken);
    }

    public async Task<bool> AvailableToUpdateAsync(int checkupId, CancellationToken cancellationToken)
    {
        var checkup = await _context
            .Checkups
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == checkupId, cancellationToken);

        return checkup != null && checkup.IsFinalized != true;
    }
}

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, int>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateReportCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var report = new Report(
            request.CheckupId,
            request.VisitNumber,
            request.HospitalNumber,
            request.VisitDate,
            request.VisitTime,
            request.CheckupTypeId,
            request.PayorName,
            request.PackageId,
            request.PackageName)
        {
            AgeCheckup = request.AgeCheckup,
            AgeTextCheckup = request.AgeTextCheckup,
            PatientId = request.PatientId,
            Weight = request.Weight,
            Height = request.Height,
            Temperature = request.Temperature,
            DiastolicBloodPresure=request.DiastolicBloodPresure,
            SystolicBloodPresure =request.SystolicBloodPresure,
            PulseRate =request.PulseRate,
            RespiratoryRate =request.RespiratoryRate,
            PhysicalExaminationBy = request.PhysicalExaminationBy,
            ConclusionBy = request.ConclusionBy,
            Conclusion = request.Conclusion,
            SpecialConclusion = request.SpecialConclusion,
            BmiReport = request.BmiReport,
            BpReport = request.BpReport,
            VisionReport = request.VisionReport,
            AudiogramReport = request.AudiogramReport,
            DentalReport = request.DentalReport,
            LungReport = request.LungReport,
            HbReport = request.HbReport,
            WbcReport = request.WbcReport,
            PlateletReport = request.PlateletReport,
            CbcReport = request.CbcReport,
            FbsReport = request.FbsReport,
            LipidReport = request.LipidReport,
            UricReport = request.UricReport,
            RenalReport = request.RenalReport,
            LiverReport = request.LiverReport,
            HepatitisReport = request.HepatitisReport,
            ThyroidReport = request.ThyroidReport,
            ImmunologyReport = request.ImmunologyReport,
            UrineReport = request.UrineReport,
            StoolReport = request.StoolReport,
        };



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
            .Where(s => s.CheckupItem?.CheckupGroup?.Code == CG.Urine && s.CheckupItem?.IsDisplayPrint==true)
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


        await _context.Reports.AddAsync(report, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return report.Id;
    }
}
