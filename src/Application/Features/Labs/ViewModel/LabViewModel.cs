using System.Text.Json.Serialization;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Labs.ViewModel;
public class LabViewModel : IMapFrom<Lab>
{
    public int Id { get; set; }
    public int CheckupId { get; set; }
    public string? VisitNumber { get; set; }
    public int CheckupItemId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CheckupItemViewModel? CheckupItem { get; set; }
    public string? ResultValue { get; set; }
    public string? ReferenceRange { get; set; }
    public string? IsAbnormal { get; set; }
    //public string? DoctorResult => IsAbnormal; 
    public string? Comments { get; set; }
    public DateOnly? ResultDate { get; set; }
    public TimeOnly ResultTime { get; set; }
    public bool DeleteFlag { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset LastModified { get; set; }

    public string? ItemCode { get; set; }
    public string? ItemName { get; set; }
    public string? GroupCode { get; set; }
    public string? GroupName { get; set; }
    public string? ClassCode { get; set; }
    public string? ClassName { get; set; }
    public string? LabItemNameRef { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Lab, LabViewModel>()
                .ForMember(d => d.ItemCode, opt => opt.MapFrom(
                    s => (s.CheckupItem != null) ? s.CheckupItem.Code : null
                    ))
                .ForMember(d => d.ItemName, opt => opt.MapFrom(
                    s => (s.CheckupItem != null) ? s.CheckupItem.DisplayName : null
                    ))
                .ForMember(d => d.GroupCode, opt => opt.MapFrom(
                    s => (s.CheckupItem != null) ? s.CheckupItem.CheckupGroup.Code : null
                    ))
                .ForMember(d => d.GroupName, opt => opt.MapFrom(
                    s => (s.CheckupItem != null) ? s.CheckupItem.CheckupGroup.Name : null
                    ))
                .ForMember(d => d.ClassCode, opt => opt.MapFrom(
                    s => (s.CheckupItem != null) ? s.CheckupItem.CheckupGroup.CheckupClass.Code : null
                    ))
                .ForMember(d => d.ClassName, opt => opt.MapFrom(
                    s => (s.CheckupItem != null) ? s.CheckupItem.CheckupGroup.CheckupClass.Name : null
                    ))
                .ForMember(d => d.LabItemNameRef, opt => opt.MapFrom(s => s.Lab_Item_Name))
                .ReverseMap();
    }
}
