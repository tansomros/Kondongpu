using System.Xml.Linq;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.Recommendations.ViewModels;

namespace Kondongpu.Application.Features.Recommendations.Queries.Get
{
  public  class GetConclutionFullByQuery : IRequest<string>
    {       
        public required string VisitNumber { get; set; }
        public required string ItemCode { get; set; }
        public required string Sexx { get; set; }
        public required string ValueType { get; set; }      
    }
    public class GetConclutionFullByQueryHandler : IRequestHandler<GetConclutionFullByQuery, string>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _context;
        public GetConclutionFullByQueryHandler(KondongpuDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<string> Handle(GetConclutionFullByQuery request, CancellationToken cancellationToken)
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
        -- คำนวณ status_code ทันที (Hard-code: valuetpid = '')
        CASE 
            WHEN '{request.ValueType}' <> 'T' AND (COALESCE(l.""IsAbnormal"", '') IN ('-', '')) THEN 'N'
            ELSE COALESCE(l.""IsAbnormal"", '')
        END AS status_code,
        c.""VisitDate"" AS v_date,
        (l.""ResultValue"" ~ '^[0-9]+(\.[0-9]+)?$') AS is_numeric
    FROM ""Labs"" l
    JOIN ""Checkups"" c ON l.""VisitNumber"" = c.""VisitNumber""
    JOIN ""CheckupItems"" ci ON l.""CheckupItemId"" = ci.""Id""
    WHERE l.""VisitNumber"" = '{request.VisitNumber}' -- <--- Hard-code: vn
      AND ci.""Code"" = '{request.ItemCode}'             -- <--- Hard-code: itemcode
)
SELECT 
    concat(r.""ConclusionTh"",' ',r.""RecommendTh"") 
FROM ""Recommendations"" r, final_status f -- Cross Join กับค่า Lab ที่มีแถวเดียว
WHERE r.""Code"" = '{request.ItemCode}'                   -- <--- Hard-code: itemcode
  AND r.""IsActive"" = true
  AND f.v_date BETWEEN r.""ActiveFrom"" AND r.""ActiveTo""
  AND (r.""SexCode"" = '' OR r.""SexCode"" = '{request.Sexx}')  -- <--- Hard-code: sexx
  AND (
    -- 1. กรณีผลปกติ
    (f.status_code = 'N' AND r.""CheckType"" = 'A') 
    OR 
    -- 2. กรณีผิดปกติ + เป็นตัวเลข
    (f.status_code <> 'N' AND f.is_numeric AND (
        (r.""CheckType"" = 'L' AND REPLACE(f.val, ',', '')::FLOAT < r.""LowValue"" AND r.""HighValue"" = 0) OR
        (r.""CheckType"" = 'B' AND REPLACE(f.val, ',', '')::FLOAT >= r.""LowValue"" AND REPLACE(f.val, ',', '')::FLOAT <= r.""HighValue"") OR
        (r.""CheckType"" = 'G' AND r.""LowValue"" = 0 AND REPLACE(f.val, ',', '')::FLOAT > r.""HighValue"")
    )) 
    OR
    -- 3. กรณีผิดปกติ + เป็นข้อความ
    (f.status_code <> 'N' AND NOT f.is_numeric AND (
        (r.""CheckType"" = 'E' AND UPPER(f.val) = UPPER(r.""CompareValue"")) OR
        (r.""CheckType"" = 'N' AND UPPER(f.val) <> UPPER(r.""CompareValue""))
    ))
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
}
