using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Dentals.ViewModels;
public class DentalViewModel : IMapFrom<Dental>
{
    public int Id { get; set; }
    public int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public int CheckupItemId { get; set; }
    public required string ResultValue { get; set; }
    public string? ResultNote { get; set; }
   
    /// <summary>
    /// เหงือกอักเสบ
    /// </summary>
    public bool Gingivitis { get; set; }
    /// <summary>
    /// ฟันผุ
    /// </summary>
    public bool Decay { get; set; }
    /// <summary>
    /// ขูดหินปูน
    /// </summary>
    public bool Scaling { get; set; }
    /// <summary>
    /// เคลือบฟลูออไรด์
    /// </summary>
    public bool Fluoride { get; set; }
    /// <summary>
    /// เคลือบหลุมร่องฟัน
    /// </summary>
    public bool Sealant { get; set; }
    public string? SealantNote { get; set; }
    /// <summary>
    /// อุดฟัน
    /// </summary>
    public bool Filling { get; set; }
    public string? FillingNote { get; set; }
    /// <summary>
    /// ถอนฟัน
    /// </summary>
    public bool Extraction { get; set; }
    public string? ExtractionNote { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Dental, DentalViewModel>();
    }
}
