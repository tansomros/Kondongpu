
using SUTH.HealthCheckup.Application.Common.Interfaces;
using SUTH.HealthCheckup.Application.Exceptions;
using SUTH.HealthCheckup.Application.Features.ReportTemplates.ViewModels;

namespace SUTH.HealthCheckup.Application.Features.ReportTemplates.Queries.CustomReport
{
    public class GetCustomReportFromSqlByDateQuery : IRequest<object>
    {
        public required long ReportId { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
    }
    internal class GetCustomReportFromSqlByDateQueryHadler : IRequestHandler<GetCustomReportFromSqlByDateQuery, object>
    {
        private readonly ICheckupDatabaseContext _checkupContext;
        private readonly IHosxpDatabaseContext _hosXpContext;
        public GetCustomReportFromSqlByDateQueryHadler(IHosxpDatabaseContext hosXpContext, ICheckupDatabaseContext suthosContext)
        {
            _hosXpContext = hosXpContext;
            _checkupContext = suthosContext;
        }

        public async Task<object> Handle(GetCustomReportFromSqlByDateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                object? result = null;
                var report = await _checkupContext.ReportTemplate.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.ReportId), cancellationToken);

                if (report != null)
                {
                    string sql = report.SqlText;

                    if (!sql.Contains("@DateStart"))
                        throw new NotFoundException("ไม่พบพารามิเตอร์ @DateStart");
                    if (!sql.Contains("@DateEnd"))
                        throw new NotFoundException("ไม่พบพารามิเตอร์ @DateEnd");

                    sql = sql.Replace($"@DateStart", string.Format("'{0}'", request.StartDate));
                    sql = sql.Replace($"@DateEnd", string.Format("'{0}'", request.EndDate));

                    result = _hosXpContext.Database.FromSqlQuerySetUTF8(sql).ToList();
                }
                return result;
                //return new CustomReportResultViewModel() { Result = result };
            }
            catch (Exception ex) 
            {
                if (ex.InnerException != null)
                    throw new NotImplementedException(ex.InnerException.Message);
                else
                    throw new NotImplementedException(ex.Message);
            }
        }
    }
}
