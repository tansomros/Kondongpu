using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.RecommendationTemplates.Commands.Create;
using Kondongpu.Application.Features.RecommendationTemplates.Commands.Delete;
using Kondongpu.Application.Features.RecommendationTemplates.Commands.Update;
using Kondongpu.Application.Features.RecommendationTemplates.Queries.Get;
using Kondongpu.Application.Features.RecommendationTemplates.Queries.GetPaginatedList;
using Kondongpu.Application.Features.RecommendationTemplates.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลแม่แบบคำแนะนำ (Recommendation Templates)
/// </summary>
public class RecommendationTemplatesController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Recommendation Template
    /// </summary>
    [HttpPost(Name = "CreateRecommendationTemplate")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateRecommendationTemplate([FromBody] CreateRecommendationTemplateCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Recommendation Template
    /// </summary>
    [HttpPut("{id}", Name = "UpdateRecommendationTemplate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateRecommendationTemplate(int id, [FromBody] UpdateRecommendationTemplateCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูล Recommendation Template ด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteRecommendationTemplate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecommendationTemplate(int id)
    {
        await Mediator.Send(new DeleteRecommendationTemplateCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Recommendation Template จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetRecommendationTemplate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecommendationTemplateViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<RecommendationTemplateViewModel>> GetRecommendationTemplate(int id)
    {
        return Ok(await Mediator.Send(new GetRecommendationTemplateQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint Recommendation Template ด้วยคำค้น ซึ่งจะ SearchTerm, Page และ Length
    /// </summary>
    [HttpGet("paginated", Name = "GetRecommendationTemplatePaginatedList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<RecommendationTemplateViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PaginatedList<RecommendationTemplateViewModel>>> GetRecommendationTemplatePaginatedList([FromQuery] GetRecommendationTemplatePaginatedListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ แม่แบบคำแนะนำ
    /// </summary>
    [HttpGet(Name = "GetRecommendationTemplateList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecommendationTemplateListViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<RecommendationTemplateListViewModel>> GetRecommendationTemplateList()
    {
        return Ok(await Mediator.Send(new GetRecommendationTemplateListQuery()));
    }
}
