using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Recommendations.Commands.Create
{
    public class CreateRecommendationCommand : IRequest<int>
    {      
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string CheckType { get; set; }
        public required string SexCode { get; set; }
        public required string CompareValue { get; set; }
        public double LowValue { get; set; }
        public double HighValue { get; set; }
        public required string ConclusionTh { get; set; }
        public required string ConclusionEn { get; set; }
        public required string RecommendTh { get; set; }
        public required string RecommendEn { get; set; }
        public bool IsActive { get; set; }
        public  DateTime? ActiveFrom { get; set; }
        public  DateTime? ActiveTo { get; set; }
    }

    public class CreateRecommendationCommmandHandler : IRequestHandler<CreateRecommendationCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateRecommendationCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
        {
            var recommendation = new Recommendation(request.Code,request.Name,request.CheckType,request.SexCode,request.CompareValue,request.LowValue,request.HighValue,request.ConclusionTh,request.ConclusionEn,request.RecommendTh,request.RecommendEn,request.ActiveFrom,request.ActiveTo)
            {               
                //SexCode = request.SexCode, 
                //CompareValue = request.CompareValue,             
                //ConclusionEn = request.ConclusionEn,
                //RecommendTh = request.RecommendTh,
                //RecommendEn = request.RecommendEn, 
                IsActive = request.IsActive,
                CreatedOn = DateTime.Now,
            };

            await _checkupDatabaseContext.Recommendations.AddAsync(recommendation, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return recommendation.Id;
        }
    }
}
