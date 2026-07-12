using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.Recommendations.Commands.Create;
using Kondongpu.Application.Features.Recommendations.Commands.Delete;
using Kondongpu.Application.Features.Recommendations.Commands.Update;
using Kondongpu.Application.Features.Recommendations.Queries.Get;
using Kondongpu.Application.Features.Recommendations.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลคำแนะนำแพทย์ (Recommendations)
/// </summary>
public class RecommendationsController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Recommendation
    /// </summary>
    [HttpPost(Name = "CreateRecommendation")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateRecommendation([FromBody] CreateRecommendationCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับอับเดต Recommendation
    /// </summary>
    [HttpPut("{id}", Name = "UpdateRecommendation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateRecommendation(int id, [FromBody] UpdateRecommendationCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลRecommendationด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteRecommendation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecommendation(int id)
    {
        await Mediator.Send(new DeleteRecommendationCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Recommendation จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetRecommendationById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecommendationViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<RecommendationViewModel>> GetRecommendationById(int id)
    {
        return Ok(await Mediator.Send(new GetRecommendationByIdQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Recommendation List ทั้งหมด
    /// </summary>
    /// <returns></returns>
    [HttpGet("list",Name = "GetRecommendationList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecommendationListViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<RecommendationListViewModel>> GetRecommendationList()
    {
        return Ok(await Mediator.Send(new GetRecommendationListQuery()));
    }

    /// <summary>
    /// API Endpoint สำหรับค้นหาข้อมูล Recommendation ด้วย VisitNumber, ItemCode, Sex, ValueType
    /// </summary>
    [HttpGet("recommend", Name = "GetRecommendation")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<string>> GetRecommendation(string vn, string itemCode, string sexx, string valueType)
    {
        return Ok(await Mediator.Send(new GetRecommendationByQuery() { VisitNumber = vn, ItemCode = itemCode, Sexx = sexx, ValueType = valueType }));
    }

    [HttpGet("compare", Name = "GetRecommendationByValue")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<string>> GetRecommendationByValue(string vn, string itemCode, string sexx, string value)
    {
        return Ok(await Mediator.Send(new GetRecommendationByValue() { VisitNumber = vn, ItemCode = itemCode, Sexx = sexx,Value = value }));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลสรุป (Conclusion) ของ Recommendation ด้วย VisitNumber, ItemCode, Sex, ValueType
    /// </summary>
    [HttpGet("conclusion", Name = "GetConclusion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<string>> GetConclusion(string vn, string itemCode, string sexx, string valueType)
    {
        return Ok(await Mediator.Send(new GetConclutionByQuery() { VisitNumber = vn, ItemCode = itemCode, Sexx = sexx, ValueType = valueType }));
    }


}
