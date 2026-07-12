using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.ReportTemplates.ViewModels
{

    public class ReportTemplateViewModel : IMapFrom<ReportTemplate>
        {
#pragma warning disable CS8618
        public int Id { get; set; }
        public string ReportName { get; set; }
        public int ReportGroupId { get; set; }
        public string ReportGroupName { get; set; } 
        public bool IsActive { get; set; }
        public string? SqlText { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
        public int Sort {  get; set; }
        public bool IsConfidential {  get; set; }

        //public HashSet<ReportTemplateDetailViewModel> ReportTemplateDetail { get; set; }

#pragma warning restore CS8618
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReportTemplate, ReportTemplateViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ReportName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.CreatedUser, o => o.MapFrom(s => s.CreateUser))
                .ForMember(d => d.ModifiedUser, o => o.MapFrom(s => s.ModifiedUser))
                .ForMember(d => d.SqlText, o => o.MapFrom(s => s.SqlText))
                .ForMember(d => d.ReportGroupId, o => o.MapFrom(s => s.ReportGroupId)) 
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive))
                  //.ForMember(d => d.Sort, o => o.MapFrom(s => s.Sort))
                .ForMember(d => d.IsConfidential, o => o.MapFrom(s => s.IsConfidential))
                .ReverseMap();
        }
    }


    public class ReportTemplateListViewModel
    {
        public ICollection<ReportTemplateViewModel> ReportTemplates { get; set; }

        public ReportTemplateListViewModel()
        {
            ReportTemplates = new HashSet<ReportTemplateViewModel>();
        }
    }

}
