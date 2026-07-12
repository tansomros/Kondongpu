namespace Kondongpu.Application.Features.Xrays.ViewModel;
public class XrayListViewModel
{
    public ICollection<XrayViewModel> Xrays { get; set; }

    public XrayListViewModel()
    {
        Xrays = [];
    }
}
