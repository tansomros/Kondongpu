using System;

namespace Kondongpu.Domain.Entities
{
    public class ReportTemplate: BaseEntity
    {
        public string Name { get; set; }
        public string? SqlText { get; set; }        
        public int ReportGroupId { get; set; }
        public ReportGroup? ReportGroup { get; set; } 
        public bool IsConfidential { get; set; }
        //public int Sort {  get; set; }
        public string? CreateUser { get; set; }
        public string? ModifiedUser { get; set; }
        public ReportTemplate(string name,int reportGroupId)
        {
            Name = name;           
            ReportGroupId = reportGroupId;    
        }

    }
}
