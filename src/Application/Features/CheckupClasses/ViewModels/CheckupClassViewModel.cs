using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupClasses.ViewModels;
public class CheckupClassViewModel : IMapFrom<CheckupClass>
{
#pragma warning disable CS8618
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Sort { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset LastModified { get; set; }
#pragma warning restore CS8618

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CheckupClass, CheckupClassViewModel>();
    }
}
