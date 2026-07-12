using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.RecommendationTemplates.ViewModels;
public class RecommendationTemplateViewModel : IMapFrom<RecommendationTemplate>
{
    public int Id { get; set; }

    #pragma warning disable CS8618
    public string Text { get; set; }
    #pragma warning restore CS8618

    public bool? DeleteFlag { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? CreatedOn { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RecommendationTemplate, RecommendationTemplateViewModel>();
    }
}

public class RecommendationTemplateListViewModel
{
    public ICollection<RecommendationTemplateViewModel> RecommendationTemplates { get; set; }
    public RecommendationTemplateListViewModel()
    {
        RecommendationTemplates = [];
    }
}
