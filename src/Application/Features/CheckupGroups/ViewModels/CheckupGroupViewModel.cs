using System.Text.Json.Serialization;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupClasses.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupGroups.ViewModels;
public class CheckupGroupViewModel : IMapFrom<CheckupGroup>
{
#pragma warning disable CS8618
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Sort { get; set; }
    public string CheckupClassCode { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CheckupClassViewModel CheckupClass { get; set; }

    public bool DeleteFlag { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset LastModified { get; set; }
#pragma warning restore CS8618

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CheckupGroup, CheckupGroupViewModel>();
    }
}
