using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Audiograms.Commands.Create;
using Kondongpu.Application.Features.Audiograms.Commands.Delete;
using Kondongpu.Application.Features.Audiograms.Commands.Update;
using Kondongpu.Application.Features.Audiograms.Queries.Get;
using Kondongpu.Application.Features.Audiograms.ViewModel;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจการได้ยิน (Audiograms)
/// </summary>
public class AudiogramsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Audiogram
    /// </summary>
    [HttpPost(Name = "CreateAudiogram")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateAudiogram([FromBody] CreateAudiogramCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Audiogram
    /// </summary>
    [HttpPut("{id}", Name = "UpdateAudiogram")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateAudiogram(int id, [FromBody] UpdateAudiogramCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลAudiogramด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteAudiogram")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAudiogram(int id)
    {
        await Mediator.Send(new DeleteAudiogramCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Audiogram จาก VisitNumber
    /// </summary>
    [HttpGet("visits/{visitNumber}", Name = "GetAudiogramByVisit")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AudiogramViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<AudiogramViewModel>> GetAudiogram(string visitNumber)
    {
        return Ok(await Mediator.Send(new GetAudiogramQuery { VisitNumber = visitNumber }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Audigram
    /// </summary>
    [HttpGet(Name = "GetAudiogramList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<AudiogramViewModel>))]
    public async Task<ActionResult<PaginatedList<AudiogramViewModel>>> GetAudiogramList([FromQuery] GetAudiogramListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับค้นหาข้อมูลAudigram ด้วยคำค้น ซึ่งจะหาจาก VisitNumber
    /// </summary>
    [HttpGet("search", Name = "SearchAudiograms")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<AudiogramViewModel>))]
    public async Task<ActionResult<PaginatedList<AudiogramViewModel>>> SearchAudiogramsByVisitNumber([FromQuery] SearchAudiogramQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
