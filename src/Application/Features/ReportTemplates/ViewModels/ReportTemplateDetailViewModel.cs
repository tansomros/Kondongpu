
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.ReportTemplates.ViewModels
{
    public class ReportTemplateDetailViewModel : IMapFrom<ReportTemplateDetail>
    {
#pragma warning disable CS8618
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string Parameters { get; set; }
        public string ControlType { get; set; }
        public string? Description { get; set; }
        public string? ValueMember { get; set; }
        public string? DisplayMember { get; set; }
        public string? Sql { get; set; }
        public string? RowAffect { get; set; }

#pragma warning restore CS8618
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReportTemplateDetail, ReportTemplateDetailViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ReportId, o => o.MapFrom(s => s.ReportTemplateId))
                .ForMember(d => d.Parameters, o => o.MapFrom(s => s.ParameterName))
                .ForMember(d => d.ControlType, o => o.MapFrom(s => s.ControlType))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.ValueMember, o => o.MapFrom(s => s.ValueMember))
                .ForMember(d => d.DisplayMember, o => o.MapFrom(s => s.DisplayMember))
                .ForMember(d => d.Sql, o => o.MapFrom(s => s.SqlText))
                .ReverseMap();
        }

    }

    public class ReportTemplateDetailListViewModel
    {
        public ICollection<ReportTemplateDetailViewModel> Details { get; set; }
        //public ICollection<int> RolesId { get; set; } = new HashSet<int>();
        public ReportTemplateDetailListViewModel()
        {
            Details = new HashSet<ReportTemplateDetailViewModel>();
        }

    }
}
