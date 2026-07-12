namespace Kondongpu.Application.Features.Checkups.ViewModels
{
    public class CheckupListViewModel
    {
        public ICollection<CheckupViewModel> Checkups { get; set; }

        public CheckupListViewModel()
        {
            Checkups = new HashSet<CheckupViewModel>();
        }
    }
}
