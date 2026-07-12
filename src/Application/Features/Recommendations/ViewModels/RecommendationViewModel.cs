using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Recommendations.ViewModels;
public class RecommendationViewModel : IMapFrom<Recommendation>
{
    public  int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string CheckType { get; set; }
    public string? SexCode { get; set; }
    public string? CompareValue { get; set; }
    public double LowValue { get; set; }
    public double HighValue { get; set; }
    public required string ConclusionTh { get; set; }
    public string? ConclusionEn { get; set; }
    public string? RecommendTh { get; set; }
    public string? RecommendEn { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Recommendation, RecommendationViewModel>();   
    }

}
