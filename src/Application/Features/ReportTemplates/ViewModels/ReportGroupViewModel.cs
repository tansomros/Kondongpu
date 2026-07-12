using AutoMapper;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;
using System;

namespace Kondongpu.Application.Features.ReportTemplates.ViewModels
{
    public class ReportGroupViewModel : IMapFrom<ReportGroup>
    {
        public int Id { get; set; }
        public string? ReportGroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReportGroup, ReportGroupViewModel>()
                .ForMember(r => r.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(r => r.ReportGroupName, opt => opt.MapFrom(d => d.Name))
                .ReverseMap();
        }
    }
}
