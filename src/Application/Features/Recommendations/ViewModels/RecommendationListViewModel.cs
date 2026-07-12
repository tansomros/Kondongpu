namespace Kondongpu.Application.Features.Recommendations.ViewModels
{
   public class RecommendationListViewModel
    {
        public ICollection<RecommendationViewModel> Recommendations { get; set; }

        public RecommendationListViewModel()
        {
            Recommendations = new HashSet<RecommendationViewModel>();
        }
    }
}
