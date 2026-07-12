using System.Xml.Linq;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Recommendations.ViewModels;

namespace Kondongpu.Application.Features.Recommendations.Queries.Get;
 
  /// <summary>
  /// สำหรับดึงค่า Recommendation เฉพาะที่ ผิดปกติ
  /// </summary>
  public  class GetRecommendationByQuery : IRequest<string>
    {       
        public required string VisitNumber { get; set; }
        public required string ItemCode { get; set; }
        public required string Sexx { get; set; }
        public required string ValueType { get; set; }      
    }
    public class GetRecommedationByQueryHandler : IRequestHandler<GetRecommendationByQuery,string>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _context;
        public GetRecommedationByQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<string> Handle(GetRecommendationByQuery request, CancellationToken cancellationToken)
        {
            
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
SELECT 
    CASE 
        WHEN f.status_code = 'N' THEN '' -- ถ้าปกติ ให้คืนค่าว่าง
        ELSE concat(r.""ConclusionTh"", ' ', r.""RecommendTh"") -- ถ้าไม่ปกติ ให้คืนข้อความ
    END
FROM ""Recommendations"" r
JOIN final_status f ON f.v_date BETWEEN r.""ActiveFrom"" AND r.""ActiveTo""
WHERE r.""Code"" = '{request.ItemCode}'                
  AND r.""IsActive"" = true
  AND (r.""SexCode"" = '' OR r.""SexCode"" = '{request.Sexx}') 
  AND (
    -- หาแถวที่เกี่ยวข้องเจอ
    (f.status_code = 'N') 
    OR 
    (f.status_code <> 'N' AND f.is_numeric AND r.""CheckType"" = 'B' AND  COALESCE(NULLIF(REPLACE(f.val::text, ',', ''), '')::FLOAT, 0) BETWEEN r.""LowValue"" AND r.""HighValue"")
    OR
    (f.status_code <> 'N' AND NOT f.is_numeric AND r.""CheckType"" = 'E' AND f.val::text ILIKE r.""CompareValue"")
)
LIMIT 1;";

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
 
