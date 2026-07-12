using System.Xml.Linq;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Recommendations.ViewModels;

namespace Kondongpu.Application.Features.Recommendations.Queries.Get
{
  public  class GetConclutionByQuery : IRequest<string>
    {       
        public required string VisitNumber { get; set; }
        public required string ItemCode { get; set; }
        public required string Sexx { get; set; }
        public required string ValueType { get; set; }      
    }
    public class GetConclutionByQueryHandler : IRequestHandler<GetConclutionByQuery, string>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _context;
        public GetConclutionByQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<string> Handle(GetConclutionByQuery request, CancellationToken cancellationToken)
        {
            //var RecommendationList = new List<RecommendationViewModel>();
            string conclusion;
            await _context.Database.OpenConnectionAsync(cancellationToken);
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $@"
                WITH final_status AS (
    SELECT 
        l.""ResultValue"" AS val,       
        CASE 
            WHEN '{request.ValueType}' <> 'T' AND (COALESCE(l.""IsAbnormal"", '') IN ('-', '')) THEN 'N' 
            ELSE COALESCE(l.""IsAbnormal"", '') 
        END AS status_code,
        c.""VisitDate"" AS v_date,        
        (REPLACE(l.""ResultValue"", ',', '') ~ '^-?[0-9]*(\.[0-9]+)?$') AS is_numeric 
    FROM ""Labs"" l
    JOIN ""Checkups"" c ON l.""VisitNumber"" = c.""VisitNumber""
    JOIN ""CheckupItems"" ci ON l.""CheckupItemId"" = ci.""Id""
    WHERE l.""VisitNumber"" = '{request.VisitNumber}' 
      AND ci.""Code"" = '{request.ItemCode}'            
)
SELECT  r.""ConclusionTh""
FROM ""Recommendations"" r
JOIN final_status f ON f.v_date BETWEEN r.""ActiveFrom"" AND r.""ActiveTo""
WHERE r.""Code"" = '{request.ItemCode}'                
  AND r.""IsActive"" = true
  AND (r.""SexCode"" = '' OR r.""SexCode"" = '{request.Sexx}') 
  AND (
	-- ถ้าเป็นตัวเลข  
    (f.is_numeric AND r.""CheckType"" = 'B' AND NULLIF(REPLACE(f.val, ',', ''), '')::FLOAT BETWEEN r.""LowValue"" AND r.""HighValue"")
    OR --ถ้าไม่ใช่ตัวเลข
    (NOT f.is_numeric AND r.""CheckType"" = 'E' AND f.val::text ILIKE r.""CompareValue"")
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
}
