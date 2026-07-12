using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.Visions.Commands.Create;
using Kondongpu.Application.Features.Visions.Commands.Delete;
using Kondongpu.Application.Features.Visions.Commands.Update;
using Kondongpu.Application.Features.Visions.Queries.Get;
using Kondongpu.Application.Features.Visions.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจสายตา (Visions)
/// </summary>
public class VisionsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Vision
    /// </summary>
    [HttpPost(Name = "CreateVision")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateVision([FromBody] CreateVisionCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Vision
    /// </summary>
    [HttpPut("{id}", Name = "UpdateVision")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateVision(int id, [FromBody] UpdateVisionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับ Update และ Insert ข้อมูล Vision
    /// </summary>
    [HttpPut("batch", Name = "UpsertVision")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpsertVision([FromBody] UpsertVisionCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลVisionด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteVision")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVision(int id)
    {
        await Mediator.Send(new DeleteVisionCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Vision จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetVision")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisionViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<VisionViewModel>> GetVision(int id)
    {
        return Ok(await Mediator.Send(new GetVisionByIdQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Vision จาก visitNumber และ checkupId
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetVisionByVisitNumber")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisionViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<VisionViewModel>> GetVisionByVisitNumber(string visitNumber, [FromQuery] int checkupId)
    {
        return Ok(await Mediator.Send(new GetVisionByVNQuery { CheckupId = checkupId, VisitNumber = visitNumber }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ สายตา
    /// </summary>
    [HttpGet(Name = "GetVisionList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisionListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<VisionListViewModel>> GetVisionList()
    {
        return Ok(await Mediator.Send(new GetVisionListQuery()));
    }
}
