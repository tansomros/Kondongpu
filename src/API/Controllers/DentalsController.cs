using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.Dentals.Commands.Create;
using Kondongpu.Application.Features.Dentals.Commands.Delete;
using Kondongpu.Application.Features.Dentals.Commands.Update;
using Kondongpu.Application.Features.Dentals.Queries.Get;
using Kondongpu.Application.Features.Dentals.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลทันตกรรม (Dental)
/// </summary>
public class DentalsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Dental
    /// </summary>
    [HttpPost(Name = "CreateDental")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateDental([FromBody] CreateDentalCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Dental
    /// </summary>
    [HttpPut("{id}", Name = "UpdateDental")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateDental(int id, [FromBody] UpdateDentalCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูล Dental ด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteDental")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDental(int id)
    {
        await Mediator.Send(new DeleteDentalCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Dental จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetDental")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DentalViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<DentalViewModel>> GetDental(int id)
    {
        return Ok(await Mediator.Send(new GetDentalByIdQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ ทันตกรรม
    /// </summary>
    [HttpGet(Name = "GetDentalList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DentalListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<DentalListViewModel>> GetDentalList()
    {
        return Ok(await Mediator.Send(new GetDentalListQuery()));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล ทันตกรรม ด้วย Visit Number
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetDentalByVisitNumber")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DentalViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<DentalViewModel>> GetDentalByVisitNumber(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetDentalByVisitNumberQuery { VisitNumber = visitNumber }));
    }
}
