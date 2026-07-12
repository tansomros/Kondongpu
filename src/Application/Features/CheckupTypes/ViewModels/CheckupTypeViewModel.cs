using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupTypes.ViewModels;
public class CheckupTypeViewModel : IMapFrom<CheckupType>
{
#pragma warning disable CS8618
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool DeleteFlag { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset LastModified { get; set; }
#pragma warning restore CS8618

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CheckupType, CheckupTypeViewModel>();
    }
}
