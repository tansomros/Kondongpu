namespace Kondongpu.Application.Features.CheckupGroups.ViewModels;
public class CheckupGroupListViewModel
{    public ICollection<CheckupGroupViewModel> CheckupGroups { get; set; }

    public CheckupGroupListViewModel()
    {
        CheckupGroups = new HashSet<CheckupGroupViewModel>();
    }
}
