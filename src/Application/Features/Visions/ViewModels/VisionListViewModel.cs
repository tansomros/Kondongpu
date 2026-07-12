namespace Kondongpu.Application.Features.Visions.ViewModels
{
   public class VisionListViewModel
    {
        public ICollection<VisionViewModel> Visions { get; set; }

        public VisionListViewModel()
        {
            Visions = new HashSet<VisionViewModel>();
        }
    }
}
