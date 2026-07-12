using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.ReportTemplates.Queries.ExecuteReport;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;

namespace Kondongpu.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomCommandController : BaseController
    {
        [HttpPost]
        [Route("ExecuteReport/{id}", Name = "ExecuteReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]

        public async Task<ActionResult<ReportViewModel>> ExecuteReport(int id, string ReportType, [FromBody] Dictionary<string, object> data)
        {
            return Ok(await Mediator.Send(new ExecuteReportQuery { ReportDetailID = id, ReportType = ReportType, Parameters = data }));
        }

        //[HttpPost]
        //[Route("QueryReport/{id}", Name = "QueryReport")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult<CustomReportResultViewModel>> QueryReport(Int32 id, [FromBody] HashSet<CustomReportParameter> parameters)
        //{            
        //    return Ok(await Mediator.Send(new GetCustomReportFromSqlQuery { ReportId = id,Parameters = parameters}));
        //}

        ///// <summary>
        ///// API Endpoint สำหรับระบบรายงานแบบวันที่
        ///// </summary>
        ///// <param name="id">รหัสรายงาน</param>
        ///// <param name="startDate">วันที่เริ่มต้น รูปแบบ YYYY-MM-DD</param>
        ///// <param name="endDate">วันที่สิ้นสุด รูปแบบ YYYY-MM-DD</param>
        ///// <remarks>ในรายงานต้องใช้พารามิเตอร์วันที่ชื่อ @StartDate และ @EndDate</remarks>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("QueryReport/{id}/{startDate}/{endDate}", Name = "QueryReportByDate")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult<object>> QueryReportByDate(Int32 id, string startDate, string endDate)
        //{
        //    return Ok(await Mediator.Send(new GetCustomReportFromSqlByDateQuery { ReportId = id, StartDate = startDate,EndDate = endDate }));
        //}
    }
}
