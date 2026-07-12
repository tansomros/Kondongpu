using Kondongpu.Domain.ValueObjects;

namespace Kondongpu.Application.Features.Checkups.ViewModels;
public class FinalReportViewModel
{
    public ICollection<FinalLabViewModel> Labs { get; set; }

    public FinalVisionViewModel? Vision { get; set; }

    public FinalReportViewModel()
    {
        Labs = [];
        Vision = null;
    }

}
