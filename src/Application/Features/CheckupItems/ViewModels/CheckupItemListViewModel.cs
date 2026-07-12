namespace Kondongpu.Application.Features.CheckupItems.ViewModels;
public class CheckupItemListViewModel
{    public ICollection<CheckupItemViewModel> CheckupItems { get; set; }

    public CheckupItemListViewModel()
    {
        CheckupItems = new HashSet<CheckupItemViewModel>();
    }
}
