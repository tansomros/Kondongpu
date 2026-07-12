using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CareProviders.ViewModels
{
    public class CareProviderViewModel : IMapFrom<CareProvider>
    {
        public int Id { get; set; }

#pragma warning disable CS8618
        public string Code { get; set; }
        public string FullNameThai { get; set; }
        public string? FullNameEnglish { get; set; }
        public string? LicenseNo { get; set; }
        public string? NationId { get; set; }
        public string? PositionName { get; set; }
        public int CareproviderTypeId { get; set; }
        public string? CareproviderTypeName { get; set; }
#pragma warning restore CS8618          
         
        public bool DeleteFlag { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CareProvider, CareProviderViewModel>()
                .ForMember(d => d.NationId, opt => opt.MapFrom(s => s.NationalId));          
        }
    }

    public class CareProviderListViewModel
    {
        public ICollection<CareProviderViewModel> CareProviders { get; set; }

        public CareProviderListViewModel()
        {
            CareProviders = new HashSet<CareProviderViewModel>();
        }
    }
}
