using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;
using Kondongpu.Domain.Enums;

namespace Kondongpu.Application.Features.BodyCompositions.ViewModels;
public class BodyCompositionViewModel : IMapFrom<BodyComposition>
{
    public int Id { get; set; }
    public int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public int CheckupItemId { get; set; }
    public string? Bmr { get; set; }
    public string? BmrNote { get; set; }
    public string? BodyWater { get; set; }
    public string? BodyWaterNote { get; set; }
    public string? VisceralFat { get; set; }
    public string? VisceralFatNote { get; set; }
    public string? BodyFat { get; set; }
    public string? BodyFatNote { get; set; }
    public bool IsActive { get; set; }

    public string? BmrText
    {
        get
        {
            switch (Bmr)
            {
                case "N":
                    return BodyResult.Normal.DisplayName;
                case "L":
                    return BodyResult.Low.DisplayName;
                case "H":
                    return BodyResult.High.DisplayName;
                default: return null;
            }
        }
    }
    public string? BodyWaterText
    {
        get
        {
            switch (BodyWater)
            {
                case "N":
                    return BodyResult.Normal.DisplayName;
                case "L":
                    return BodyResult.Low.DisplayName;
                case "H":
                    return BodyResult.High.DisplayName;
                default: return null;
            }
        }
    }
    public string? VisceralFatText
    {
        get
        {
            switch (VisceralFat)
            {
                case "N":
                    return BodyResult.Normal.DisplayName;
                case "L":
                    return BodyResult.Low.DisplayName;
                case "H":
                    return BodyResult.High.DisplayName;
                default: return null;
            }
        }
    }
    public string? BodyFatText
    {
        get
        {
            switch (BodyFat)
            {
                case "N":
                    return BodyResult.Normal.DisplayName;
                case "L":
                    return BodyResult.Low.DisplayName;
                case "H":
                    return BodyResult.High.DisplayName;
                default: return null;
            }
        }
    }
    public string? FatRate { get; set; }
    public string? FatRateNote { get; set; }
    public string? MuscleMass { get; set; }
    public string? MuscleMassNote { get; set; }

    public string? FatRateText
    {
        get
        {
            switch (FatRate)
            {
                case "N":
                    return BodyResult.Normal.DisplayName;
                case "L":
                    return BodyResult.Low.DisplayName;
                case "H":
                    return BodyResult.High.DisplayName;
                default: return null;
            }
        }
    }
    public string? MuscleMassText
    {
        get
        {
            switch (MuscleMass)
            {
                case "N":
                    return BodyResult.Normal.DisplayName;
                case "L":
                    return BodyResult.Low.DisplayName;
                case "H":
                    return BodyResult.High.DisplayName;
                default: return null;
            }
        }
    }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<BodyComposition, BodyCompositionViewModel>();
    }
}
