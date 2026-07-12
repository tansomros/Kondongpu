using System.Text.Json;
using System.Text.Json.Serialization;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Audiograms.ViewModel;
using Kondongpu.Application.Features.BodyCompositions.ViewModels;
using Kondongpu.Application.Features.Checkups.Constants;
using Kondongpu.Application.Features.CheckupTypes.ViewModels;
using Kondongpu.Application.Features.Companies.ViewModels;
using Kondongpu.Application.Features.Dentals.ViewModels;
using Kondongpu.Application.Features.Labs.ViewModel;
using Kondongpu.Application.Features.Lungs.ViewModels;
using Kondongpu.Application.Features.Patients.ViewModels;
using Kondongpu.Application.Features.PhysicalExaminations.ViewModels;
using Kondongpu.Application.Features.SpecialTests.ViewModel;
using Kondongpu.Application.Features.Visions.ViewModels;
using Kondongpu.Application.Features.Xrays.ViewModel;
using Kondongpu.Domain.Entities;
using Kondongpu.Domain.ValueObjects;

namespace Kondongpu.Application.Features.Checkups.ViewModels
{
    public class CheckupViewModel : IMapFrom<Checkup>
    {
        //#pragma warning disable CS8618
        public int Id { get; set; }
        public int CheckupVisitId { get; set; }
        public required string VisitNumber { get; set; }
        public required string HospitalNumber { get; set; }
        public int? AgeCheckup { get; set; }
        public string? AgeTextCheckup { get; set; }
        public DateOnly VisitDate { get; set; }
        public TimeOnly VisitTime { get; set; }
      
        public int CheckupTypeId { get; set; }
        public CheckupTypeViewModel? CheckupType { get; set; }
        public int Status { get; set; }
        public DateTime? SaveDate { get; set; }
        public bool IsFinalized { get; set; }
        public DateTime? FinalizedDate { get; set; }
        public string? StatusName { 
            get
            {
                switch (Status)
                {
                    case CheckupStatusConstants.Pending:
                        return "รอตรวจ" ;
                    case CheckupStatusConstants.InProgress:
                        return "กำลังตรวจ";
                    case CheckupStatusConstants.Completed:
                        return "รายงานผล";
                    default:
                        return "รอตรวจ";
                }
            }
        }
        public string? FinalStatus
        {
            get
            {
                if (IsFinalized)
                {
                    return "Finalized";
                }
                else
                {
                    switch (Status)
                    {
                        case CheckupStatusConstants.Pending:
                            return "Pending";
                        case CheckupStatusConstants.InProgress:
                            return "Pending";
                        case CheckupStatusConstants.Completed:
                            return "Reviewed";
                        default:
                            return "Pending";
                    }
                }
                   
            }
        }

        public int PhysicalExaminationById { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CareProvider? PhysicalExaminationBy { get; set; }
        public string? Conclusion { get; set; }
        public int ConclusionById { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CareProvider? ConclusionBy { get; set; }
        public string? PayorName { get; set; }
        public int PackageId { get; set; }
        public string? PackageName { get; set; }
        public bool IsLabResultReady { get; set; }
        public string? LabStatusName
        {
            get
            {
                switch (IsLabResultReady)
                {
                    case false:
                        return "Awaiting Results";
                    case true:                    
                        return "Completed";                   
                }
            }
        }
        public bool IsXrayResultReady { get; set; }
        public string? XrayStatusName
        {
            get
            {
                switch (IsXrayResultReady)
                {
                    case false:
                        return "Awaiting Results";
                    case true:
                        return "Completed";
                }
            }
        }

   

        public double Weight { get; set; }
        public double Height { get; set; }

        public double Bmi => Math.Round(Weight / Math.Pow((Height/ 100),2),2);

        public string? Waist { get; set; }
        public string? Hips { get; set; }

        public double Temperature { get; set; }
        public int? PulseRate { get; set; }
        public int? SystolicBloodPresure { get; set; }
        public int? DiastolicBloodPresure { get; set; }
        public int? RespiratoryRate { get; set; }
        public string? PhysicalExaminationConclusion { get; set; }
        public string? LabResultConclusion { get; set; }
        public string? XrayResultConclusion { get; set; }
        public string? SpecialConclusion { get; set; }
        public bool IsMain { get; set; } = true;

        public bool? IsSmoking { get; set; }
        public string? SmokingRemark { get; set; }
        public string? Smoking
        {
            get
            {
                switch (IsSmoking)
                {
                    case true:
                        return "สูบ "+ SmokingRemark;
                    case false:
                        return "ไม่สูบ "+ SmokingRemark;
                    default:return null;
                }
            }
        }       
      
        public bool? IsAlcohol { get; set; }
        public string? AlcoholRemark { get; set; }
        public string? Alcohol
        {
            get
            {
                switch (IsAlcohol)
                {
                    case true:
                        return "ดื่ม " + AlcoholRemark;
                    case false:
                        return "ไม่ดื่ม " + AlcoholRemark;
                    default: return null;
                }
            }
        }

        public int? CompanyId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CompanyViewModel? Company { get; set; }

        public ICollection<BodyCompositionViewModel>? BodyCompositions { get; set; }
        public ICollection<LabViewModel>? Labs { get; set; }
        public ICollection<XrayViewModel>? Xrays { get; set; }
        public ICollection<VisionViewModel>? Visions { get; set; }
        public ICollection<AudiogramViewModel>? Audiograms { get; set; }
        public ICollection<DentalViewModel>? Dentals { get; set; }
        public ICollection<LungViewModel>? Lungs { get; set; }
        public ICollection<PhysicalExaminationViewModel>? PhysicalExams { get; set; }
        public ICollection<SpecialTestViewModel>? SpecialTests { get; set; }
        public List<string>? LabOrder {  get; set; }
        public List<string>? XrayOrder { get; set; }
        public List<string>? ServiceOrder { get; set; }
        public bool DeleteFlag { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public PatientViewModel? Patient { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //[SwaggerSchema(Nullable = true)]
        public FinalReport? FinalReport { get; set; }

        //public int AgeYear { get; set; }
        //public required string AgeText { get; set; }

        public bool HasVisionOrder { get; set; }
        public bool HasVisionResult { get; set; }
        public string VisionStatusName { get; set; } = string.Empty;


        public bool HasDentalOrder { get; set; }
        public bool HasDentalResult { get; set; }
        public string DentalStatusName { get; set; } = string.Empty;


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Checkup, CheckupViewModel>()
            .ForSourceMember(s => s.Company, opt => opt.DoNotValidate());

            //.ForMember(d => d.AgeYear, opt => opt.MapFrom<DateResolver>())
            //.ForMember(d => d.AgeText, opt => opt.MapFrom<AgeTextResolver>())
            //.ReverseMap();
        }

    //    public static class OrderServiceCodes
    //    {
    //        public static readonly HashSet<string> Vision = new()
    //{
    //    "3046050",
    //    "3046051",
    //    "3046053"
    //};
    //    }


        //class DateResolver : IValueResolver<Checkup, CheckupViewModel, int>
        //{
        //    public int Resolve(Checkup checkup, CheckupViewModel destination, int destMember, ResolutionContext context)
        //    {
        //        if (destination.Patient != null)
        //        {

        //            DateOnly vstDate = destination.VisitDate;
        //            DateOnly birthDate = destination.Patient.BirthDate;

        //            int years = vstDate.Year - birthDate.Year;
        //            int months = vstDate.Month - birthDate.Month;
        //            int days = vstDate.Day - birthDate.Day;

        //            if (days < 0)
        //            {
        //                months--;
        //                DateOnly prevMonth = vstDate.AddMonths(-1);
        //                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
        //            }

        //            if (months < 0)
        //            {
        //                years--;
        //                months += 12;
        //            }

        //            return years; // $"{years} ปี {months} เดือน {days} วัน";
        //        }

        //        return 0;
        //    }
        //}

        //class AgeTextResolver : IValueResolver<Checkup, CheckupViewModel, string>
        //{         
        //    public string Resolve(Checkup source, CheckupViewModel destination, string destMember, ResolutionContext context)
        //    {
        //        if (destination.Patient != null)
        //        {
        //            DateOnly vstDate = destination.VisitDate;
        //            DateOnly birthDate = destination.Patient.BirthDate;

        //            int years = vstDate.Year - birthDate.Year;
        //            int months = vstDate.Month - birthDate.Month;
        //            int days = vstDate.Day - birthDate.Day;

        //            if (days < 0)
        //            {
        //                months--;
        //                DateOnly prevMonth = vstDate.AddMonths(-1);
        //                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
        //            }

        //            if (months < 0)
        //            {
        //                years--;
        //                months += 12;
        //            }

        //            return $"{years} ปี {months} เดือน {days} วัน";
        //        }

        //        return string.Empty;
        //    }
        //}
    }
}
