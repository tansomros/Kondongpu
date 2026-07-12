using System.Text.Json.Serialization;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupClasses.ViewModels;
using Kondongpu.Application.Features.CheckupGroups.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupItems.ViewModels
{
    public class CheckupItemViewModel : IMapFrom<CheckupItem>
    {
#pragma warning disable CS8618
        public int Id { get; set; }
        public string Code { get; set; }
        public string? LabItemCode { get; set; }
        public string DisplayName { get; set; }
        public string? Description { get; set; }
        public string? CumulativeName { get; set; }
        public string? CumulativeGroup { get; set; }  
        public int CheckupGroupId { get; set; }
        public int CumulativeSort { get; set; }
        public int Sort { get; set; }
        public bool IsDisplayPrint { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CheckupGroupViewModel? CheckupGroup { get; set; }        
        public bool DeleteFlag { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastModified { get; set; }
#pragma warning restore CS8618

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CheckupItem, CheckupItemViewModel>();
        }
    }
}
