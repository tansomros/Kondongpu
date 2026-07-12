using System;

namespace Kondongpu.Domain.Entities
{
    public class ReportTemplateDetail: BaseEntity
    {
        public int ReportTemplateId { get; set; }
        public ReportTemplate? ReportTemplate { get; set; }
        public string ParameterName { get; set; }
        public string ControlType { get; set; }
        public string? Description { get; set; }
        public string? ValueMember { get; set; }
        public string? DisplayMember { get; set; }
        public string? SqlText { get; set; }
        public string? CreateUser { get; set; }
        public string? ModifiedUser { get; set; }
        public ReportTemplateDetail(int reportTemplateId,string parameterName,string controlType) 
        {
            ReportTemplateId = reportTemplateId;
            ParameterName = parameterName;
            ControlType = controlType;   
        }

    }
}
