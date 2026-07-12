using Kondongpu.Application.Features.ReportTemplates.ViewModels;

namespace Kondongpu.Application.Features.ReportTemplates.Queries.ExecuteReport
{
    public class ExecuteReportQuery : IRequest<ReportViewModel>
    {
        public Int32 ReportID { get; set; }
        public int ReportDetailID { get; set; }
        public string? ReportType { get; set; }
        public Dictionary<string, object>? Parameters { get; set; }
    }
}
