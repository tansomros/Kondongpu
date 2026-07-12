using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Reports.Commands;
using Kondongpu.Application.Features.Reports.Queries;
using Kondongpu.Application.Features.Reports.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับรวบรวมและออกรายงานผลการตรวจสุขภาพ (Reports)
/// </summary>
public class ReportsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับสร้างรายงานผลตรวจ
    /// </summary>
    [HttpPost(Name = "CreateReport")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดตรายงานผลตรวจ
    /// </summary>
    [HttpPut("{visitNumber}", Name = "UpdateReport")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateReport(string visitNumber, [FromBody] UpdateReportCommand command)
    {
        if (visitNumber != command.VisitNumber)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงรายงานผลตรวจของผู้เข้ารับบริการ ด้วย HN
    /// </summary>
    [HttpGet("patients/{hospitalNumber}", Name = "PatientReportByHN")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientReportViewModel))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PatientReportViewModel>> PatientReportByHN(string hospitalNumber)
    {
        return Ok(await Mediator.Send(new GetPatientReportByHospitalNumberQuery { HospitalNumber = hospitalNumber }));
    }

    /// <summary>
    /// สำหรับใช้ในใบรายงานผลตรวจสุขภาพ
    /// </summary>
    [HttpGet("checkups/{visitNumber}", Name = "CheckupReport")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupReportViewModel))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CheckupReportViewModel>> CheckupReport(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetCheckupReportByVisitNumberQuery { VisitNumber = visitNumber }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล รายงานผลการตรวจสุขภาพ จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetCheckupReport")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupReportViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CheckupReportViewModel>> GetCheckupReport(int id)
    {
        return Ok(await Mediator.Send(new GetCheckupReportQuery { Id = id }));
    }

    /// <summary>
    /// การดึงข้อมูลรายการรายงานผลการตรวจสุขภาพด้วย HN
    /// </summary>
    [HttpGet("paginated", Name = "GetCheckupReportPaginatedList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<CheckupReportViewModel>))]
    public async Task<ActionResult<PaginatedList<CheckupReportViewModel>>> GetCheckupReportPaginatedList([FromQuery] GetCheckupReportPaginatedListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ รายงานผลตรวจสุขภาพ
    /// </summary>
    [HttpGet(Name = "GetReportList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupReportListViewModel))]
    public async Task<ActionResult<CheckupReportListViewModel>> GetReportList([FromQuery] GetReportListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
