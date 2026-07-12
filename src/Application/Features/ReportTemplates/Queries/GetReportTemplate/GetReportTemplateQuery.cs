
using MediatR;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.ReportTemplates.Queries
{
    public class GetReportTemplateQuery : IRequest<ReportTemplateViewModel>
    {
        public int UID { get; set; }
    }
     public class GetReportTemplateHandler : IRequestHandler<GetReportTemplateQuery, ReportTemplateViewModel>
    {
        private readonly KondongpuDatabaseContext _checkupContext;
        private readonly IMapper _mapper;

        public GetReportTemplateHandler(KondongpuDatabaseContext context, IMapper mapper)
        {
            _checkupContext = context;
            _mapper = mapper;
        }
        public async Task<ReportTemplateViewModel> Handle(GetReportTemplateQuery request, CancellationToken cancellationToken)
        {
            var report = await _checkupContext.ReportTemplate.AsNoTracking()
                .Where(r => r.Id.Equals(request.UID))
                .ProjectTo<ReportTemplateViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            //var report = await _context.ReportTemplates
            //        .Join(_context.ReportGroups,
            //        rt => rt.ReportGroupUID,
            //        rg => rg.UID,
            //        (rt, rg) => new AllReportTemplateViewModel
            //        {
            //            Id = rt.UID,
            //            ReportName = rt.Name,
            //            ReportGroupId = rt.ReportGroupUID,
            //            ReportGroupName = rg.Name,
            //            ReportType = rt.Type,
            //            ReportFromDb = rt.Db,
            //            IsPublic = rt.IsPublic,
            //            Sql = rt.SqlText
            //        })
            //        //.ProjectTo<ReportTemplateViewModel>(_mapper.ConfigurationProvider)
            //        .ToListAsync(cancellationToken);

            if (report == null)
            {
                throw new NotFoundException(nameof(ReportTemplate), request.UID);
            }
            //else
            //{
            //    report.ReportGroup = reportGroup.ReportGroupName;
            //}


            //using (var command = _hosxpContext.DbConnection.CreateCommand())
            //{                
            //    command.CommandText = report.sql;                
            //    await _hosxpContext.DbConnection.OpenAsync(cancellationToken);
            //    using (var result = command.ExecuteReader())
            //    {
            //        while (result.Read())
            //        {

            //        }
            //    }
            //}

            return report;
        }
    }
}
