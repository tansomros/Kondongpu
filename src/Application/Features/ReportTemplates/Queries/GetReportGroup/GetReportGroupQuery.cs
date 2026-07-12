using MediatR;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;
using System.Collections.Generic;

namespace Kondongpu.Application.Features.ReportTemplates.Queries
{
    public class GetReportGroupQuery : IRequest<List<ReportGroupViewModel>>
    {
    }
}
