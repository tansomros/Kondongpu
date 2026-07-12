using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.CheckupItems.Queries.Get;
using Kondongpu.Application.Features.PhysicalExaminations.Commands.Create;
using Kondongpu.Application.Features.PhysicalExaminations.Commands.Delete;
using Kondongpu.Application.Features.PhysicalExaminations.Commands.Update;
using Kondongpu.Application.Features.PhysicalExaminations.Queries.Get;
using Kondongpu.Application.Features.PhysicalExaminations.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจร่างกาย (Physical Examinations)
/// </summary>
public class PhysicalExaminationsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม PhysicalExaminations
    /// </summary>
    [HttpPost(Name = "CreatePhysicalExaminations")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreatePhysicalExamination([FromBody] CreatePhysicalExaminationCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต PhysicalExaminations
    /// </summary>
    [HttpPut("{id}", Name = "UpdatePhysicalExamination")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdatePhysicalExamination(int id, [FromBody] UpdatePhysicalExaminationCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลPhysicalExaminationด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeletePhysicalExamination")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePhysicalExamination(int id)
    {
        await Mediator.Send(new DeletePhysicalExaminationCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล PhysicalExaminations จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetPhysicalExamination")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhysicalExaminationViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PhysicalExaminationViewModel>> GetPhysicalExamination(int id)
    {
        return Ok(await Mediator.Send(new GetByIdPhysicalExaminationQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ การตรวจร่างกาย
    /// </summary>
    [HttpGet(Name = "GetPhysicalExaminationList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhysicalExaminationViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PhysicalExaminationViewModel>> GetPhysicalExaminationList()
    {
        return Ok(await Mediator.Send(new GetListPhysicalExaminationQuery()));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลการตรวจร่างกาย ด้วย Visit Number
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetPhysicalExaminationByVisitNumber")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhysicalExaminationViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PhysicalExaminationViewModel>> GetPhysicalExaminationByVisitNumber(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetPhysicalExaminationByVisitNumberQuery { VisitNumber = visitNumber }));
    }  
}
