using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.CheckupGroups.Commands.Create;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลกลุ่มผลการตรวจ (Checkup Groups)
/// </summary>
public class CheckupGroupsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม CheckupGroup
    /// </summary>
    [HttpPost(Name = "CreateCheckupGroup")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateCheckupGroup([FromBody] CreateCheckupGroupCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }
}
