namespace Kondongpu.Application.Features.Lungs.ViewModels
{
   public class LungListViewModel
    {
        public ICollection<LungViewModel> Lungs { get; set; }

        public LungListViewModel()
        {
            Lungs = new HashSet<LungViewModel>();
        }
    }
}
