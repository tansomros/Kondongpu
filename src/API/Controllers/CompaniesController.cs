using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.Companies.Commands.Create;
using Kondongpu.Application.Features.Companies.Commands.Delete;
using Kondongpu.Application.Features.Companies.Commands.Update;
using Kondongpu.Application.Features.Companies.Queries.Get;
using Kondongpu.Application.Features.Companies.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลบริษัท/สถานประกอบการ (Companies)
/// </summary>
public class CompaniesController : BaseController
{

    /// <summary>
    /// API Endpoint สำหรับเพิ่ม Company
    /// </summary>
    [HttpPost(Name = "CreateCompany")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        var id = await Mediator.Send(command);
        return Created($"{Request.Path.Value}/{id}", id);
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Company
    /// </summary>
    [HttpGet(Name = "GetCompanyList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyListViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CompanyListViewModel>> GetCompanyList()
    {
        return Ok(await Mediator.Send(new GetCompanyListQuery()));
    }

    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูล Company จาก Id
    /// </summary>
    [HttpGet("{id}", Name = "GetCompany")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CompanyViewModel>> GetCompany(int id)
    {
        return Ok(await Mediator.Send(new GetCompanyByIdQuery { Id = id }));
    }
    

    /// <summary>
    /// API Endpoint สำหรับอับเดต Company
    /// </summary>
    [HttpPut("{id}", Name = "UpdateCompany")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// API Endpoint สำหรับลบข้อมูลCompanyด้วย Id
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteCompany")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        await Mediator.Send(new DeleteCompanyCommand { Id = id });
        return NoContent();
    }


}
