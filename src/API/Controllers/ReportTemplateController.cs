using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.Companies.Queries.Get;
using Kondongpu.Application.Features.ReportTemplates.Commands.Create;
using Kondongpu.Application.Features.ReportTemplates.Commands.Update;
using Kondongpu.Application.Features.ReportTemplates.Queries;
using Kondongpu.Application.Features.ReportTemplates.ViewModels;

namespace Kondongpu.Presentation.API.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
    public class ReportTemplateController : BaseController
    {
        [HttpGet("ReportTemplateId/{id}", Name = "GetReportTemplateId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]

        public async Task<ActionResult<ReportTemplateViewModel>> GetReportTemplateId(int id)
        {
            return Ok(await Mediator.Send(new GetReportTemplateQuery { UID = id }));
        }


    [HttpGet("ReportTemplateDetail/{id}", Name = "GetReportTemplateDetail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]

    public async Task<ActionResult<ReportTemplateDetailListViewModel>> GetReportTemplateDetail(int id)
    {
        return Ok(await Mediator.Send(new GetReportTemplateDetailQuery { ReportTemplateId = id }));
    }

        [HttpGet]
        [Route("ReportTemplateList", Name = "GetReportTemplateList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportTemplateListViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))] 
        public async Task<ActionResult<ReportTemplateListViewModel>> GetReportTemplateList()
        { 
            return Ok(await Mediator.Send(new GetAllReportTemplateQuery()));
        }



    [HttpGet]
        [Route("ReportGroupList", Name = "GetReportGroupList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]

        public async Task<ActionResult<List<ReportGroupViewModel>>> GetReportGroupList()
        {
            return Ok(await Mediator.Send(new GetReportGroupQuery { }));
        }

    
    //[HttpPost]
    //[Route("ExecuteReport")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]

    //public async Task<IActionResult> ExecuteReport(int Id,[FromBody]Dictionary<string,object> data)
    //{
    //    return Ok(await Mediator.Send(new ExecuteReportQuery { ReportID = Id, Parameters = data })); ;
    //}

    [HttpPut]
        [Route("UpdateReportTemplate", Name = "UpdateReportTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<ActionResult<ReportTemplateViewModel>> UpdateReportTemplate(UpdateReportTemplateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Route("CreateReportTemplate", Name = "CreateReportTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<ActionResult<ReportTemplateViewModel>> Upsert(CreateReportTemplateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [Route("DeleteReportTemplate",Name = "DeleteReportTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<ActionResult<ReportTemplateViewModel>> Delete(GetReportTemplateQuery command)
        {
            return Ok(await Mediator.Send(command));
        }
    } 
