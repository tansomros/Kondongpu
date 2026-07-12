using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.CareProviders.Commands.Create;
using Kondongpu.Application.Features.CareProviders.Commands.Update;
using Kondongpu.Application.Features.CareProviders.Queries.Get;
using Kondongpu.Application.Features.CareProviders.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลผู้ให้บริการ/แพทย์ (Care Providers)
/// </summary>
public class CareProvidersController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Care Provider
    /// </summary>
    [HttpPost(Name = "CreateCareProvider")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateCareProvider([FromBody] CreateCareProviderCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับดึง Care Provider ด้วย Id
    /// </summary>
    [HttpGet("{id}", Name = "GetCareProvider")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CareProviderViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CareProviderViewModel>> GetCareProvider(int id)
    {
        return Ok(await Mediator.Send(new GetCareProviderQuery { Id = id }));
    }

    /// <summary>
    /// API Endpoint สำหรับ Update และ Insert เป็นชุดข้อมูล CareProvider ลิสต์จาก HosXP
    /// </summary>
    [HttpPut("sync/worker", Name = "UpsertLatestUpdateProviderForWorker")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpsertLatestUpdateProviderForWorker([FromBody] UpsertLatestUpdateProviderForWorkerCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Care Provider
    /// </summary>
    [HttpGet(Name = "GetCareProviderList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CareProviderListViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CareProviderListViewModel>> GetCareProviderList()
    {
        return Ok(await Mediator.Send(new GetCareProviderListQuery()));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Care Provider ตามประเภท (Type)
    /// </summary>
    [HttpGet("types/{careProviderTypeId}", Name = "GetCareProviderByType")] 
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CareProviderListViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CareProviderListViewModel>> GetCareProviderByTypeQuery(int careProviderTypeId)
    {
        return Ok(await Mediator.Send(new GetCareProviderByTypeQuery { CareProviderTypeId = careProviderTypeId }));
    }
}
