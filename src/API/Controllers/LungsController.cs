using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.Lungs.Commands.Create;
using Kondongpu.Application.Features.Lungs.Commands.Delete;
using Kondongpu.Application.Features.Lungs.Commands.Update;
using Kondongpu.Application.Features.Lungs.Queries.Get;
using Kondongpu.Application.Features.Lungs.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจสมรรถภาพปอด (Lungs)
/// </summary>
public class LungsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Lung
    /// </summary>
    [HttpPost(Name = "CreateLung")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateLung([FromBody] CreateLungCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Lung
    /// </summary>
    [HttpPut("{id}", Name = "UpdateLung")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateLung(int id, [FromBody] UpdateLungCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลLungด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteLung")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLung(int id)
    {
        await Mediator.Send(new DeleteLungCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Lung จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetLung")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LungViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<LungViewModel>> GetLung(int id)
    {
        return Ok(await Mediator.Send(new GetLungByIdQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ สมรรถภาพปอด
    /// </summary>
    [HttpGet(Name = "GetLungList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LungListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<LungListViewModel>> GetLungList()
    {
        return Ok(await Mediator.Send(new GetLungListQuery()));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล สมรรถภาพปอด ด้วย Visit Number
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetLungByVisitNumber")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LungViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<LungViewModel>> GetLungByVisitNumber(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetLungByVisitNumberQuery { VisitNumber = visitNumber }));
    }
}
