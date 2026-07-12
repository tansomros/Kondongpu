using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.CheckupClasses.Commands.Create;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลคลาสผลการตรวจ (Checkup Classes)
/// </summary>
public class CheckupClassesController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม CheckupClass
    /// </summary>
    [HttpPost(Name = "CreateCheckupClass")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateCheckupClass([FromBody] CreateCheckupClassCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }
}
