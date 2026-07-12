using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Commands.Create
{
    // input หรือ request
    public class CreateCheckupCommand : IRequest<int>
    {
        public required int PatientId { get; set; }
        public required int CheckupVisitId { get; set; }
        public required string VisitNumber { get; set; }
        public required string HospitalNumber { get; set; }
        public required DateOnly VisitDate { get; set; }
        public required TimeOnly VisitTime { get; set; }
        public required int CheckupTypeId { get; set; }
        public required string PayorName { get; set; }
        public required int PackageId { get; set; }
        public required string PackageName { get; set; }
        public bool IsLabResultReady { get; set; }
        public bool IsXrayResultReady { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Temperature { get; set; }
        public int? DiastolicBloodPresure { get; set; }
        public int? SystolicBloodPresure { get; set; }
        public int? PulseRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }

        public int? Age {  get; set; }
        public string? AgeText { get; set; }

        public required List<string> LabOrder { get; set; }
        public required List<string> XrayOrder { get; set; }
        public required List<string> ServiceOrder { get; set; }

        public bool? IsSmoking { get; set; }
        public string? SmokingRemark { get; set; }
        public bool? IsAlcohol { get; set; }
        public string? AlcoholRemark { get; set; }

    }

    // เอา INPUT มา Process
    public class CreateVisitCommmandHandler : IRequestHandler<CreateCheckupCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateVisitCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateCheckupCommand request, CancellationToken cancellationToken)
        {
            var checkupItemCode = await _checkupDatabaseContext.CheckupItems
                .Select(s => s.LabItemCode)
                .ToListAsync(cancellationToken);

            var visit = new Checkup(
                request.CheckupVisitId,
                request.VisitNumber,
                request.HospitalNumber,
                request.VisitDate,
                request.VisitTime,
                request.CheckupTypeId,
                request.PayorName,
                request.PackageId,
                request.PackageName)
            {
                AgeCheckup = request.Age,
                AgeTextCheckup = request.AgeText,
                PatientId = request.PatientId,
                Status = CheckupStatusConstants.Pending,
                IsLabResultReady = request.IsLabResultReady,
                IsXrayResultReady = request.IsXrayResultReady,
                Weight = request.Weight,
                Height = request.Height,
                Temperature = request.Temperature,
                IsMain = true,
            };

            visit.DiastolicBloodPresure = request.DiastolicBloodPresure;
            visit.SystolicBloodPresure = request.SystolicBloodPresure;
            visit.PulseRate = request.PulseRate;
            visit.RespiratoryRate = request.RespiratoryRate;

            //visit.LabOrder = request.LabOrder.Where(checkupItemCode.Contains).ToList(); 
            //visit.XrayOrder = request.XrayOrder.Where(checkupItemCode.Contains).ToList();
            //visit.ServiceOrder = request.ServiceOrder.Where(checkupItemCode.Contains).ToList();

            visit.Waist = request.Waist;
            visit.Hips = request.Hips;
            visit.IsSmoking = request.IsSmoking;
            visit.SmokingRemark = request.SmokingRemark;
            visit.IsAlcohol = request.IsAlcohol;
            visit.AlcoholRemark = request.AlcoholRemark;

            await _checkupDatabaseContext.Checkups.AddAsync(visit, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return visit.Id;
        }


    }
}
