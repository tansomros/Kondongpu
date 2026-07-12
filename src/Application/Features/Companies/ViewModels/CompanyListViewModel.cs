namespace Kondongpu.Application.Features.Companies.ViewModels
{
   public class CompanyListViewModel
    {
        public ICollection<CompanyViewModel> Companies { get; set; }

        public CompanyListViewModel()
        {
            Companies = new HashSet<CompanyViewModel>();
        }
    }
}
