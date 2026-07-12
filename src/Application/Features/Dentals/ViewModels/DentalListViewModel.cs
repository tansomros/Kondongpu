namespace Kondongpu.Application.Features.Dentals.ViewModels
{
    public class DentalListViewModel
    {
        public ICollection<DentalViewModel> Dentals { get; set; }

        public DentalListViewModel()
        {
            Dentals = new HashSet<DentalViewModel>();
        }
    }
}
