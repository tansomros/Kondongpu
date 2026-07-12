using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.SpecialTests.Commands.Create;
using Kondongpu.Application.Features.SpecialTests.Commands.Delete;
using Kondongpu.Application.Features.SpecialTests.Commands.Update;
using Kondongpu.Application.Features.SpecialTests.Queries.Get;
using Kondongpu.Application.Features.SpecialTests.ViewModel;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจพิเศษ (Special Tests)
/// </summary>
public class SpecialTestController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม SpecialTest
    /// </summary>
    [HttpPost(Name = "CreateSpecialTest")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateSpecialTest([FromBody] CreateSpecialTestCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต SpecialTest
    /// </summary>
    [HttpPut("{id}", Name = "UpdateSpecialTest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateSpecialTest(int id, [FromBody] UpdateSpecialTestCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }
   
    /// <summary>
    /// API Endpoint สำหรับอับเดต SpecialTest Batch
    /// </summary>
    [HttpPut("batch", Name = "UpdateCreateSpecialTestList")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateCreateSpecialTestList([FromBody] UpdateCreateSpecialTestListCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลSpecialTestด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteSpecialTest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteSpecialTest(int id)
    {
        await Mediator.Send(new DeleteSpecialTestCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล SpecialTest จาก visitNumber
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetSpecialTest")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecialTestViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<SpecialTestViewModel>> GetSpecialTest(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetSpecialTestQuery { VisitNumber = visitNumber }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ SpecialTest
    /// </summary>
    [HttpGet(Name = "GetSpecialTestList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<SpecialTestViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PaginatedList<SpecialTestViewModel>>> GetSpecialTestList([FromQuery] GetSpecialTestListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับค้นหาข้อมูลSpecialTest ด้วย VisitNumber ซึ่งจะหาจาก Code และ Name
    /// </summary>
    [HttpGet("search", Name = "SearchSpecialTest")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<SpecialTestViewModel>))]
    public async Task<ActionResult<PaginatedList<SpecialTestViewModel>>> SearchSpecialTest([FromQuery] SearchSpecialTestQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
