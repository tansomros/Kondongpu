namespace Kondongpu.Application.Features.Reports.ViewModels
{
    public class CheckupReportListViewModel
    {
        public ICollection<CheckupReportViewModel> Reports { get; set; }

        public CheckupReportListViewModel()
        {
            Reports = new HashSet<CheckupReportViewModel>();
        }
    }
}
