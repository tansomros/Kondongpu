namespace Kondongpu.Application.Features.PhysicalExaminations.ViewModels
{
   public class PhysicalExaminationListViewModel
    {
        public ICollection<PhysicalExaminationListViewModel> PhysicalExaminations { get; set; }

        public PhysicalExaminationListViewModel()
        {
            PhysicalExaminations = new HashSet<PhysicalExaminationListViewModel>();
        }
    }
}
