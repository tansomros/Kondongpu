using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Hearings.Commands.Create;
using Kondongpu.Application.Features.Hearings.Commands.Delete;
using Kondongpu.Application.Features.Hearings.Commands.Update;
using Kondongpu.Application.Features.Hearings.Queries.Get;
using Kondongpu.Application.Features.Hearings.ViewModel;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจการได้ยิน (Hearings)
/// </summary>
public class HearingController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Hearing
    /// </summary>
    [HttpPost(Name = "CreateHearing")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateHearing([FromBody] CreateHearingCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Hearing List
    /// </summary>
    [HttpPost("batch", Name = "CreateHearingList")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateHearingList([FromBody] CreateHearingListCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Hearing
    /// </summary>
    [HttpPut("{id}", Name = "UpdateHearing")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateHearing(int id, [FromBody] UpdateHearingCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลHearingด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteHearing")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteHearing(int id)
    {
        await Mediator.Send(new DeleteHearingCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Hearing จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetHearing")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HearingViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<HearingViewModel>> GetHearing(int id)
    {
        return Ok(await Mediator.Send(new GetHearingQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Hearing
    /// </summary>
    [HttpGet(Name = "GetHearingList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<HearingViewModel>))]
    public async Task<ActionResult<PaginatedList<HearingViewModel>>> GetHearingList([FromQuery] GetHearingListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับค้นหาข้อมูล Hearing ด้วย ระบุ AudiogramId
    /// </summary>
    [HttpGet("search", Name = "GetHearingListByAudigramId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<HearingViewModel>))]
    public async Task<ActionResult<PaginatedList<HearingViewModel>>> GetHearingListByAudigramId([FromQuery] GetHearingListByAudigramIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
