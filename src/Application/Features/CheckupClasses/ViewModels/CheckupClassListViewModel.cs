namespace Kondongpu.Application.Features.CheckupClasses.ViewModels;
public class CheckupClassListViewModel
{   
    public ICollection<CheckupClassViewModel> CheckupClasses { get; set; }

    public CheckupClassListViewModel()
    {
        CheckupClasses = new HashSet<CheckupClassViewModel>();
    }
}
