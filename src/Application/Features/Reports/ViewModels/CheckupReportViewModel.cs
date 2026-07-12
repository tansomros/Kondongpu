using System.Text.Json.Serialization;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Application.Features.CheckupTypes.ViewModels;
using Kondongpu.Application.Features.Patients.ViewModels;
using Kondongpu.Domain.Entities;
using Kondongpu.Domain.ValueObjects;

namespace Kondongpu.Application.Features.Reports.ViewModels
{
    public class CheckupReportViewModel : IMapFrom<Report>
    {
#pragma warning disable CS8618

        public int CheckupId { get; set; }
        public string VisitNumber { get; set; }
        public string HospitalNumber { get; set; }
        /// <summary>
        /// อายุ เป็น ปี จนถึงวันเข้ารับบริการ
        /// </summary>
        public int? AgeCheckup { get; set; }
        /// <summary>
        /// อายุ เป็น ปี เดือน วัน จนถึงวันเข้ารับบริการ
        /// </summary>
        public string? AgeTextCheckup { get; set; }
        public int PatientId { get; set; } 

        /// <summary>
        /// วันที่รับบริการ นำข้อมูลมาจากตาราง ovst คอลัม vstdate ซึ่งเป็น DateOnly
        /// </summary>
        public DateOnly VisitDate { get; set; }     

        /// <summary>
        /// เวลาที่มารับบริการ นำข้อมูลมาจากตาราง ovst คอลัม vsttime ซึ่งเป็น TimeOnly
        /// </summary>
        public string VisitTime { get; set; }
        public string VisitDttm =>  DisplayStr2ShortDateTH(VisitDate.ToString("dd-MM-yyyy")) +" "+ VisitTime.Substring(0,5);
        /// <summary>
        /// ประเภทการตรวจสุขภาพ
        /// </summary>
        public int CheckupTypeId { get; set; }      

        /// <summary>
        /// แพทย์ผู้ตรวจร่างกาย
        /// </summary>
        public string? PhysicalExaminationBy { get; set; }

        /// <summary>
        /// แพทย์ผู้สรุปผล
        /// </summary> 
        public string? ConclusionBy { get; set; }

        /// <summary>
        /// สรุปผลการตรวจจากแพทย์
        /// </summary>
        public string? Conclusion { get; set; }

        /// <summary>
        /// ชื่อสิทธิ์การรักษา ใช้ชื่อสิทธิ์ของผู้รับบริการจาก HOSxP เลย
        /// </summary>
        public string PayorName { get; set; }

        /// <summary>
        /// ไอดี โปรแกรมตรวจสุขภาพ จากตาราง ckup_prog คอลัม ckup_prog_id ใน HOSxP
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// ชื่อโปรแกรมตรวจสุขภาพ จากตาราง ckup_prog คอลัม ckup_prog_name ใน HOSxP
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// น้ำหนัก (กิโลกรัม)
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// ส่วนสูง (เซ็นติเมตร)
        /// </summary>
        public double Height { get; set; }

        public double Bmi => Math.Round( Weight / Math.Pow((Height / 100), 2),2);

        /// <summary>
        /// อุณหภูมิร่างกาย (องศาเซลเซียส)
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// อัตราการเต้นของหัวใจ - ชีพจร (ครั้งต่อนาที)
        /// </summary>
        public int? PulseRate { get; set; }

        /// <summary>
        /// ความดันโลหิตตัวหน้า (mm/Hg)
        /// </summary>
        public int? SystolicBloodPresure { get; set; }

        /// <summary>
        /// ความดันโลหิตตัวหลัง (mm/Hg)
        /// </summary>
        public int? DiastolicBloodPresure { get; set; }

    

        /// <summary>
        /// อัตราการหายใจ (ต่อนาที)
        /// </summary>
        public int? RespiratoryRate { get; set; }

        /// <summary>
        /// สรุปผลการตรวจร่างกาย
        /// </summary>
        public string? PhysicalExaminationConclusion { get; set; }

        /// <summary>
        /// สรุปผล Lab
        /// </summary>
        public string? LabResultConclusion { get; set; }

        /// <summary>
        /// สรุปผล Xray
        /// </summary>
        public string? XrayResultConclusion { get; set; }

        /// <summary>
        /// สรุปผลแลบพิเศษอื่นๆ
        /// </summary>
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

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  
        public LabReport? Wbc { get; set; }
        public LabReport? Fbs { get; set; }
        public LabReport? Lipid { get; set; }
        public LabReport? Renal { get; set; }
        public LabReport? Liver { get; set; }
        public LabReport? Urine { get; set; }
        public LabReport? Stool { get; set; }
        public LabReport? OtherLab { get; set; }

        //public ICollection<LabViewModel>? Labs { get; set; }
        //public ICollection<XrayViewModel>? Xrays { get; set; }
        //public ICollection<VisionViewModel>? Visions { get; set; }
        //public ICollection<AudiogramViewModel>? Audiograms { get; set; }
        //public ICollection<LungViewModel>? Lungs { get; set; }
        //public ICollection<PhysicalExaminationViewModel>? PhysicalExams { get; set; }
        //public ICollection<SpecialTestViewModel>? SpecialTests { get; set; }

        public CheckupTypeViewModel? CheckupType { get; set; }
        public PatientViewModel? Patient { get; set; }
        public CheckupViewModel? Checkup { get; set; }


#pragma warning restore CS8618

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Report, CheckupReportViewModel>();
        }

        public static string DisplayStr2ShortDateTH(string dt)
        {
            if (!string.IsNullOrEmpty(dt))
            {
                string dd = dt.Substring(0, 2);
                string mm = dt.Substring(3, 2);
                string yy = dt.Substring(6, 4);

                while (dd.Length < 2)
                {
                    dd = "0" + dd;

                }

                while (mm.Length < 2)
                {
                    mm = "0" + mm;

                }
                if (Convert.ToInt32(yy) < 2500)
                {
                    yy = (Convert.ToInt32(yy) + 543).ToString();
                }

                return dd + "-" + mm + "-" + yy;
            }
            else
            {
                return "";
            }

        }
    }   
}
