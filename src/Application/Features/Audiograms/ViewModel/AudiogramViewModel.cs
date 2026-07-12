using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Application.Features.Hearings.ViewModel;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Audiograms.ViewModel;
public class AudiogramViewModel : IMapFrom<Audiogram>
{
    public int Id { get; set; }

    public int CheckupId { get; set; }
    public string? VisitNumber { get; set; }
    public int CheckupItemId { get; set; }
    //public CheckupItemViewModel? CheckupItem { get; set; }
    public string? LeftNote { get; set; }
    public string? RightNote { get; set; }
    public string? LeftResult { get; set; }
    public string? RightResult { get; set; }
    public string? ResultNote { get; set; }
    public ICollection<HearingViewModel>? Hearings { get; set; }

    public bool DeleteFlag { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset LastModified { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Audiogram, AudiogramViewModel>();   
    }

}
