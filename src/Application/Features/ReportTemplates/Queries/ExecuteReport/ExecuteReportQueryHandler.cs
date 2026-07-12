using System.Data;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Extensions.EfHelper;
//using Kondongpu.Application.Features.ActivityLog.Report.Commands;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.ReportTemplates.Queries.ExecuteReport;

public class ExecuteReporQueryHandler : IRequestHandler<ExecuteReportQuery, ReportViewModel>
{
    private readonly KondongpuDatabaseContext _checkupContext; 

    private readonly IMapper _mapper;
    //private ActivityLogReportCreateCommandHandler activityLog;
    public ExecuteReporQueryHandler(KondongpuDatabaseContext context,IMapper mapper)
    {
        _checkupContext = context;
        _mapper = mapper;
        //activityLog = new ActivityLogReportCreateCommandHandler(context);            
    }
    public async Task<ReportViewModel> Handle(ExecuteReportQuery request, CancellationToken cancellationToken)
    {
        //string client_encoding = "SET client_encoding TO 'UTF-8';";
        string sql = "";
        if (request.ReportID > 0)
        {
            var reportTemplate = await _checkupContext.ReportTemplate
                .Where(r => r.Id.Equals(request.ReportID))
                .ProjectTo<ReportTemplateViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (reportTemplate == null)
                throw new NotFoundException(nameof(ReportTemplateViewModel), request.ReportID);

            sql = reportTemplate.SqlText?? "";
            request.ReportType = "CHECKUP";
        }
        else
        {
            var report = await _checkupContext.ReportTemplateDetail
                .Where(r => r.Id.Equals(request.ReportDetailID))
                .ProjectTo<ReportTemplateDetailViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (report == null)
                throw new NotFoundException(nameof(ReportTemplateDetailViewModel), request.ReportDetailID);
            sql = report.Sql??""; 
        }

        // Replace parameters in SQL
        if (request.Parameters != null)
        {
            foreach (KeyValuePair<string, object> p in request.Parameters)
            {
                var valueStr = p.Value?.ToString() ?? "";
                if (p.GetType() == typeof(int))
                    sql = sql.Replace(p.Key.ToString(), valueStr);
                else
                {
                    if (valueStr.Contains("'"))
                        sql = sql.Replace(p.Key.ToString(), string.Format("{0}", valueStr));
                    else
                        sql = sql.Replace(p.Key.ToString(), string.Format("'{0}'", valueStr));
                }
            }
        }

        var reportViewModel = new ReportViewModel();

        if (request.ReportType == "SUTHOS")
        {
            reportViewModel.DataResult = _checkupContext.Database.FromSqlQuery(sql).ToList();
        }
        else
        {
            reportViewModel.DataResult = _checkupContext.Database.FromSqlQuery(sql).ToList();
        }

        //ActivityLogReportCreateCommand log = new ActivityLogReportCreateCommand();
        //log.ReportName = reportTemplate.ReportName;
        //log.Operation = "VIEW";
        //log.UserId = request.UserCode;
        //activityLog.CreateLog(log);

        return reportViewModel;
    }

}
