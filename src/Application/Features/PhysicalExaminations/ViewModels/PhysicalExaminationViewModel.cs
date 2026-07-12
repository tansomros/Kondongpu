using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Application.Features.Checkups.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.PhysicalExaminations.ViewModels;
public class PhysicalExaminationViewModel : IMapFrom<PhysicalExamination>
{
    public int Id { get; set; }
    public int CheckupId { get; set; }
    public string? VisitNumber { get; set; }
    public int CheckupItemId { get; set; }

    public string? Ga { get; set; }
    public string? Heent { get; set; }
    public string? Mouth { get; set; }
    public string? Lymph { get; set; }
    public string? Thyroid { get; set; }
    public string? Chest { get; set; }
    public string? Heart { get; set; }
    public string? Abdomen { get; set; }
    public string? Ext { get; set; }
    public string? Skin { get; set; }
    public string? Other { get; set; }

    public string? GaText { get; set; }
    public string? HeentText { get; set; }
    public string? MouthText { get; set; }
    public string? LymphText { get; set; }
    public string? ThyroidText { get; set; }
    public string? ChestText { get; set; }
    public string? HeartText { get; set; }
    public string? AbdomenText { get; set; }
    public string? ExtText { get; set; }
    public string? SkinText { get; set; }
    public string? OtherText { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<PhysicalExamination, PhysicalExaminationViewModel>();   
    }

}
