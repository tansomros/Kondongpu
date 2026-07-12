namespace Kondongpu.Application.Features.CheckupTypes.ViewModels
{
    public class CheckupTypeListViewModel
    {
        public ICollection<CheckupTypeViewModel> CheckupTypes { get; set; }

        public CheckupTypeListViewModel()
        {
            CheckupTypes = new HashSet<CheckupTypeViewModel>();
        }
    }
}
