using System.Text.Json.Serialization;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Domain.Entities;
using Kondongpu.Domain.Enums;

namespace Kondongpu.Application.Features.Xrays.ViewModel;
public class XrayViewModel : IMapFrom<Xray>
{
    public int Id { get; set; }

    public int CheckupId { get; set; }
    public string? VisitNumber { get; set; }
    public int CheckupItemId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CheckupItemViewModel? CheckupItem { get; set; }
    public string? ResultValue { get; set; }
    public string? ResultDisplayName { get; set; }
    public string? IsAbnormal { get; set; }
    public string? ResultReport { get; set; }
    public string? ReportText { get; set; }

    public string? AccessionNumber { get; set; }

    public bool? DeleteFlag { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? CreatedOn { get; set; }
    public DateTimeOffset? LastModified { get; set; }

    public string? ItemCode { get; set; }
    public string? ItemName { get; set; }
    public string? GroupCode { get; set; }
    public string? GroupName { get; set; }
    public string? ClassCode { get; set; }
    public string? ClassName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Xray, XrayViewModel>()
            .ForMember(d => d.ResultDisplayName, opt => opt.Ignore())
            .AfterMap((s, d) =>
            {
                if (s.ResultValue == null || s.CheckupItem == null) return;

                var code = s.CheckupItem.Code;
                d.ResultDisplayName = code switch
                {
                    "ABI" => AbiResult.TryFromValue(s.ResultValue, out var abi) ? abi!.DisplayName : null,
                    "BMD" => BmdResult.TryFromValue(s.ResultValue, out var bmd) ? bmd!.DisplayName : null,
                    "X" or "UWA" or "UUA" or "ECH" or "MAMO" or "UB" or "EKG" or "EST"
                        => XrayResult.TryFromValue(s.ResultValue, out var xr) ? xr!.DisplayName : null,
                    _ => null
                };
            })
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
                .ReverseMap();
    }

}

