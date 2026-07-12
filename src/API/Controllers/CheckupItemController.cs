using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.CheckupItems.Commands.Create;
using Kondongpu.Application.Features.CheckupItems.Commands.Delete;
using Kondongpu.Application.Features.CheckupItems.Commands.Update;
using Kondongpu.Application.Features.CheckupItems.Queries.Get;
using Kondongpu.Application.Features.CheckupItems.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลรายการผลการตรวจ (Checkup Items)
/// </summary>
public class CheckupItemController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม CheckupItem
    /// </summary>
    [HttpPost(Name = "CreateCheckupItem")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateCheckupItem([FromBody] CreateCheckupItemCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต CheckupItem
    /// </summary>
    [HttpPut("{id}", Name = "UpdateCheckupItem")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateCheckupItem(int id, [FromBody] UpdateCheckupItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูล CheckupItem ด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteCheckupItem")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteCheckupItem(int id)
    {
        await Mediator.Send(new DeleteCheckupItemCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล CheckupItem จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetCheckupItem")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupItemViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CheckupItemViewModel>> GetCheckupItem(int id)
    {
        return Ok(await Mediator.Send(new GetCheckupItemQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล CheckupItem จาก Lab xray service order
    /// </summary>
    [HttpGet(Name = "GetCheckupItemList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupItemListViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CheckupItemListViewModel>> GetCheckupItemList([FromQuery] GetCheckupItemListQuery command)
    {
        return Ok(await Mediator.Send(command));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล CheckupItem List จาก checkupClassCode
    /// </summary>
    [HttpGet("classes/{checkupClassCode}", Name = "GetCheckupItemsByClass")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupItemListViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CheckupItemListViewModel>> GetCheckupItemsByClass(string checkupClassCode)
    {
        return Ok(await Mediator.Send(new GetCheckupItemByClassQuery { CheckupClassCode = checkupClassCode }));
    }
}
