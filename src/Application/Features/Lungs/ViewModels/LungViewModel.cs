using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Lungs.ViewModels;
public class LungViewModel : IMapFrom<Lung>
{
    public int Id { get; set; }

    public int CheckupId { get; set; }
    //public virtual CheckupViewModel? Checkup { get; set; }
    public required string VisitNumber { get; set; }
    public int CheckupItemId { get; set; }
    //public CheckupItemViewModel? CheckupItem { get; set; }
    public double FVC { get; set; }
    public double? FVC_Rate { get; set; }
    public double? FEV1 { get; set; }
    public double? FEV1_Rate { get; set; }
    public string? ResultAbnormal { get; set; }
    public string? RestrictionAbnormal { get; set; }
    public string? RestrictionLevel { get; set; }
    public string? ObstructionAbnormal { get; set; }
    public string? ObstructionLevel { get; set; }
    public string? CombineAbnormal { get; set; }

    public string? IsConsult { get; set; }
    public string? ResultNote { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Lung, LungViewModel>();   
    }

}
