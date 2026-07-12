namespace Kondongpu.Application.Features.Audiograms.ViewModel;
public class AudiogramListViewModel
{
    public ICollection<AudiogramViewModel> Audiograms { get; set; }
    public AudiogramListViewModel()
    {
        Audiograms = [];
    }
}
