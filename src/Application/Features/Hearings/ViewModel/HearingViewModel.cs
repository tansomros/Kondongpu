using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Hearings.ViewModel;
public class HearingViewModel : IMapFrom<Hearing>
{
    public int Id { get; set; }

    public int AudiogramId { get; set; }
    public int Hertz { get; set; }
    public double LeftHz { get; set; }
    public double RightHz { get; set; }

    public bool DeleteFlag { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset LastModified { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Hearing, HearingViewModel>();
    }
}
