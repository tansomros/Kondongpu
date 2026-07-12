using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Companies.ViewModels;
public class CompanyViewModel : IMapFrom<Company>
{
    public int? CompanyUid { get; set; }
    public string CompanyCode { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? AliasName { get; set; }
    public string? OwnerName { get; set; }
    public string? VatId { get; set; }
    public string? AddressNo { get; set; }
    public string? Moo { get; set; }
    public string? Village { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public int? ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? StatusFlag { get; set; }
    public int? CUser { get; set; }
    public DateTime? CWhen { get; set; }
    public int? MUser { get; set; }
    public DateTime? MWhen { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Company, CompanyViewModel>();
    }
}
