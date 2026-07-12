namespace Kondongpu.Application.Features.BodyCompositions.ViewModels
{
    public class BodyCompositionListViewModel
    {
        public ICollection<BodyCompositionViewModel> BodyCompositions { get; set; }

        public BodyCompositionListViewModel()
        {
            BodyCompositions = new HashSet<BodyCompositionViewModel>();
        }
    }
}
