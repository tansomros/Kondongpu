using System.Text.Json.Serialization;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Visions.ViewModels;
public class VisionViewModel : IMapFrom<Vision>
{
    public int Id { get; set; }
    public int CheckupId { get; set; }
    public int CheckupItemId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CheckupItemViewModel? CheckupItem { get; set; }
    public string? VA_Right_Value { get; set; }
    public string? VA_Left_Value { get; set; }
    public string? PH_Right_Value { get; set; }
    public string? PH_Left_Value { get; set; }

#pragma warning disable CS8618
    public string VisitNumber { get; set; }
    public string VisionRightResult { get; set; }
    public string VisionLeftResult { get; set; }
#pragma warning restore CS8618
    public string? ColorBlind { get; set; }
    public string? PressureRight { get; set; }
    public string? PressureLeft { get; set; }
    public string? Vision3D { get; set; }
    public string? Squint { get; set; }
    public string? VisualField { get; set; }
    public string? RetinaRight { get; set; }
    public string? RetinaLeft { get; set; }
    public string? ResultNote { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vision, VisionViewModel>();
    }

}
