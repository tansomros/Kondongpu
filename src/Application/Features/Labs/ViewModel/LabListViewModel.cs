namespace Kondongpu.Application.Features.Labs.ViewModel;
public class LabListViewModel
{
    public ICollection<LabViewModel> Labs { get; set; }

    public LabListViewModel()
    {
        Labs = [];
    }
}
