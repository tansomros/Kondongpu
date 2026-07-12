
using SUTH.HealthCheckup.Application.Common.Interfaces;
using SUTH.HealthCheckup.Application.Features.ReportTemplates.ViewModels;
using System.Data;

namespace SUTH.HealthCheckup.Application.Features.ReportTemplates.Queries.CustomReport
{
    public class GetCustomReportFromSqlQuery : IRequest<CustomReportResultViewModel>
    {
        public required long ReportId { get; set; }
        public HashSet<CustomReportParameter> Parameters { get; set; }
    }
    internal class GetCustomReportFromSqlQueryHandler : IRequestHandler<GetCustomReportFromSqlQuery, CustomReportResultViewModel>
    {
        private readonly ICheckupDatabaseContext _checkupContext;
        private readonly IHOSxPDatabaseContext _hosXpContext;
        private readonly IHOSxPInventoryContext _invContext;
        private readonly IBackBonDatabaseContext _backbonContext;
        private readonly IRxScanDatabaseContext _rxscanContext;
        public GetCustomReportFromSqlQueryHandler(ICheckupDatabaseContext suthosContext, 
            IHOSxPDatabaseContext hosxpContext,
            IHOSxPInventoryContext invContext,
            IBackBonDatabaseContext backBonContext,
            IRxScanDatabaseContext rxscanContext
            )
        {
            _checkupContext = suthosContext;
            _hosXpContext = hosxpContext;
            _invContext = invContext;
            _backbonContext = backBonContext;
            _rxscanContext = rxscanContext;
        }
        public async Task<CustomReportResultViewModel> Handle(GetCustomReportFromSqlQuery request, CancellationToken cancellationToken)
        {
            try
            {
                object? result = null;
                var report = await _checkupContext.ReportTemplate.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.ReportId), cancellationToken);
                if (report != null)
                {
                    // string sql = "SET client_encoding TO 'UTF-8';\n\r";
                    //bool useDblink = false;
                    string sql = report.SqlText;
                    //useDblink = sql.Contains("dblink");

                    foreach (var param in request.Parameters)
                    {
                        sql = sql.Replace($"@{param.Name}", string.Format("'{0}'", param.Value.ToString()));
                    }

                    if (report.DatabaseSource == "HOSxP")
                        result = _hosXpContext.Database.FromSqlQuerySetUTF8(sql).ToList();
                    else if (report.DatabaseSource == "HOSxP_INV")
                    {
                        result = _invContext.Database.FromSqlQuerySetUTF8(sql).ToList();
                    }
                    else if (report.DatabaseSource == "BackBone")
                        result = _backbonContext.Database.FromSqlQuery(sql).ToList();
                    else if(report.DatabaseSource == "SUTHos")
                    {
                        result = _checkupContext.Database.FromSqlQuery(sql).ToList();
                        //if (useDblink)
                        //{
                        //    List<string> builder = new List<string>();
                        //    var dbString = _invContext.Database.GetConnectionString().Split(';');
                        //    foreach(var str  in dbString)
                        //    {
                        //        if (str.Contains("Host")) builder.Add(str.Replace("Host","host"));
                        //        if (str.Contains("Port")) builder.Add(str.Replace("Port", "port"));
                        //        if (str.Contains("Username")) builder.Add(str.Replace("Username", "user"));
                        //        if (str.Contains("Password")) builder.Add(str.Replace("Password", "password"));
                        //        if (str.Contains("Database")) builder.Add(str.Replace("Database", "dbname"));
                        //    }
                        //    sql = sql.Replace("@INV", string.Join(" ", builder));
                        //    result = _checkupContext.Database.FromSqlQuerySetUTF8(sql).ToList();
                        //}
                    }
                    else if (report.DatabaseSource == "RxScan")
                    {
                        result = _rxscanContext.Database.FromSqlQuery(sql).ToList();
                    }
                }
                return new CustomReportResultViewModel { Result = result };
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
