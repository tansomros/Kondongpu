using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Recommendations.ViewModels;

namespace Kondongpu.Application.Features.Recommendations.Queries.Get;
/// <summary>
/// สำหรับส่งค่ามาเปรียบเทียบกับ Recommendation ตรง  เลย ไม่ต้องดึงค่าจาก Labs
/// </summary>
public class GetRecommendationByValue : IRequest<string>
{
    public required string VisitNumber { get; set; }
    public required string ItemCode { get; set; }
    public required string Sexx { get; set; }
    public required string Value { get; set; }  
}
public class GetRecommendationByValueHandler : IRequestHandler<GetRecommendationByValue, string>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;
    public GetRecommendationByValueHandler(KondongpuDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Handle(GetRecommendationByValue request, CancellationToken cancellationToken)
    {
        //var RecommendationList = new List<RecommendationViewModel>();
        string conclusion;
        await _context.Database.OpenConnectionAsync(cancellationToken);
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = $@"WITH final_status AS (
    SELECT {request.Value} AS val, c.""VisitDate"" AS v_date
    FROM ""Checkups"" c  
    WHERE c.""VisitNumber"" = '{request.VisitNumber}' 
) SELECT  concat(r.""ConclusionTh"", ' ', r.""RecommendTh"") 
FROM ""Recommendations"" r JOIN final_status f ON f.v_date BETWEEN r.""ActiveFrom"" AND r.""ActiveTo""
WHERE r.""Code"" = '{request.ItemCode}'                
  AND r.""IsActive"" = true
  AND (r.""SexCode"" = '' OR r.""SexCode"" = '{request.Sexx}') 
  AND (
    (r.""CheckType"" = 'B' AND COALESCE(NULLIF(REPLACE(f.val::text, ',', ''), '')::FLOAT, 0) BETWEEN r.""LowValue"" AND r.""HighValue"")
    OR
    (r.""CheckType"" = 'E' AND f.val::text ILIKE r.""CompareValue"")
)
LIMIT 1;
";

            using var result = await command.ExecuteReaderAsync(cancellationToken);
            if (!result.HasRows)
            {
                //throw new NotFoundException("Recommendation", request.VisitNumber);
            }
            await result.ReadAsync(cancellationToken);
            try
            {
                conclusion = result.IsDBNull(0) ? "" : result.GetString(0);
            }
            catch
            {
                conclusion = "";
            }
        }

        await _context.Database.CloseConnectionAsync();
        return conclusion;

    }

} 
