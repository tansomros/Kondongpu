using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.HearingHertzs.ViewModel;
public class HearingHertzViewModel : IMapFrom<HearingHertz>
{
    public int Id { get; set; }
    public int Hertz { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<HearingHertz, HearingHertzViewModel>();
    }
}
