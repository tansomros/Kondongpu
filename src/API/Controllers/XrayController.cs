using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Xrays.Commands.Create;
using Kondongpu.Application.Features.Xrays.Commands.Delete;
using Kondongpu.Application.Features.Xrays.Commands.Update;
using Kondongpu.Application.Features.Xrays.Queries.Get;
using Kondongpu.Application.Features.Xrays.ViewModel;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจเอกซเรย์ (X-Rays)
/// </summary>
public class XrayController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Xray
    /// </summary>
    [HttpPost(Name = "CreateXray")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateXray([FromBody] CreateXrayCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Xray
    /// </summary>
    [HttpPut("{id}", Name = "UpdateXray")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateXray(int id, [FromBody] UpdateXrayCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }
    /// <summary>
    /// API Endpoint สำหรับอับเดต Xray Batch
    /// </summary>
    [HttpPut("update/batch", Name = "UpdateXrayList")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateXrayList([FromBody] UpdateXrayListCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// API Endpoint สำหรับอับเดต Xray Batch
    /// </summary>
    [HttpPut("batch", Name = "UpdateCreateXrayList")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateCreateXrayList([FromBody] UpdateCreateXrayListCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
      

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลXrayด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteXray")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteXray(int id)
    {
        await Mediator.Send(new DeleteXrayCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Xray จาก visitNumber
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetXray")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(XrayViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<XrayViewModel>> GetXray(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetXrayQuery { VisitNumber = visitNumber }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Xray
    /// </summary>
    [HttpGet(Name = "GetXrayList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(XrayListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<XrayListViewModel>> GetXrayList([FromQuery] GetXrayListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับค้นหาข้อมูลXray ด้วยคำค้น ซึ่งจะหาจาก VisitNumber
    /// </summary>
    [HttpGet("search", Name = "SearchXray")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<XrayViewModel>))]
    public async Task<ActionResult<PaginatedList<XrayViewModel>>> SearchXray([FromQuery] SearchXrayQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Xray ทีมี Code และ Name ของ CheckItem, CheckupGroup, CheckipClass ร่วมด้วย
    /// </summary>
    [HttpGet("visits/{visitNumber}/with-classes", Name = "GetXrayListWithClass")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(XrayListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<XrayListViewModel>> GetXrayListWithClass(string visitNumber)
    {
        var query = new GetXrayListWithClassQuery { VisitNumber = visitNumber };
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับ Update และ Insert เป็นชุดข้อมูล Xray ลิสต์จาก HosXP
    /// </summary>
    [HttpPut("sync/worker", Name = "UpsertLatestXrayForWorker")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpsertLatestXrayForWorker([FromBody] UpsertLatestXrayForWorkerCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}
