using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.BodyCompositions.Commands.Create;
using Kondongpu.Application.Features.BodyCompositions.Commands.Delete;
using Kondongpu.Application.Features.BodyCompositions.Commands.Update;
using Kondongpu.Application.Features.BodyCompositions.Queries.Get;
using Kondongpu.Application.Features.BodyCompositions.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลองค์ประกอบร่างกาย (Body Composition)
/// </summary>
public class BodyCompositionsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม BodyComposition
    /// </summary>
    [HttpPost(Name = "CreateBodyComposition")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateBodyComposition([FromBody] CreateBodyCompositionCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต BodyComposition
    /// </summary>
    [HttpPut("{id}", Name = "UpdateBodyComposition")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateBodyComposition(int id, [FromBody] UpdateBodyCompositionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูล BodyComposition ด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteBodyComposition")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBodyComposition(int id)
    {
        await Mediator.Send(new DeleteBodyCompositionCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล BodyComposition จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetBodyComposition")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BodyCompositionViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<BodyCompositionViewModel>> GetBodyComposition(int id)
    {
        return Ok(await Mediator.Send(new GetBodyCompositionByIdQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ องค์ประกอบร่างกาย
    /// </summary>
    [HttpGet(Name = "GetBodyCompositionList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BodyCompositionListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<BodyCompositionListViewModel>> GetBodyCompositionList()
    {
        return Ok(await Mediator.Send(new GetBodyCompositionListQuery()));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล องค์ประกอบร่างกาย ด้วย Visit Number
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetBodyCompositionByVisitNumber")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BodyCompositionViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<BodyCompositionViewModel>> GetBodyCompositionByVisitNumber(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetBodyCompositionByVisitNumberQuery { VisitNumber = visitNumber }));
    }
}
