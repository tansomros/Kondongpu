using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Labs.Commands.Create;
using Kondongpu.Application.Features.Labs.Commands.Delete;
using Kondongpu.Application.Features.Labs.Commands.Update;
using Kondongpu.Application.Features.Labs.Queries.Get;
using Kondongpu.Application.Features.Labs.ViewModel;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลผลการตรวจทางห้องปฏิบัติการ (Labs)
/// </summary>
public class LabsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Lab
    /// </summary>
    [HttpPost(Name = "CreateLab")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateLab([FromBody] CreateLabCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Lab
    /// </summary>
    [HttpPut("{id}", Name = "UpdateLab")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateLab(int id, [FromBody] UpdateLabCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับ Update และ Create เป็นชุดข้อมูล Lab ลิสต์
    /// </summary>
    [HttpPut("batch", Name = "BatchUpsertLabs")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateCreateLabList([FromBody] UpdateCreateLabListCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับ Update และ Insert เป็นชุดข้อมูล Lab ลิสต์จาก HosXP
    /// </summary>
    [HttpPut("sync/worker", Name = "SyncLabsFromWorker")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpsertLatestLabForWorker([FromBody] UpsertLatestLabForWorkerCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลLabด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteLab")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteLab(int id)
    {
        await Mediator.Send(new DeleteLabCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Lab จาก visitNumber
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetLabByVisit")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LabViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<LabViewModel>> GetLab(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetLabQuery { VisitNumber = visitNumber }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Lab
    /// </summary>
    [HttpGet(Name = "GetLabList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<LabViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PaginatedList<LabViewModel>>> GetLabList([FromQuery] GetLabListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Lab ทีมี Code และ Name ของ CheckItem, CheckupGroup, CheckipClass ร่วมด้วย
    /// </summary>
    [HttpGet("visits/{visitNumber}/with-classes", Name = "GetLabListWithClass")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LabListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<LabListViewModel>> GetLabListWithClass(string visitNumber)
    {
        var query = new GetLabListWithClassQuery { VisitNumber = visitNumber };
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Lab ตาม GroupCode โดยอ้างอิงจาก VisitNumber
    /// </summary>
    [HttpGet("visits/{visitNumber}/groups/{groupCode}", Name = "GetLabListByGroup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LabListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<LabListViewModel>> GetLabListByGroup(string visitNumber, string groupCode)
    {
        var query = new GetLabListByGroupQuery { VisitNumber = visitNumber, GroupCode = groupCode };
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับค้นหาข้อมูลLab ด้วยคำค้น ซึ่งจะหาจาก VisitNumber
    /// </summary>
    [HttpGet("search", Name = "SearchLabByVisitNumber")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<LabViewModel>))]
    public async Task<ActionResult<PaginatedList<LabViewModel>>> SearchLabByVisitNumber([FromQuery] SearchLabQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
