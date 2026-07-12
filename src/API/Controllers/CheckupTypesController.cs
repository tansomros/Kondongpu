using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Features.CheckupTypes.Queries.Get;
using Kondongpu.Application.Features.CheckupTypes.ViewModels;

namespace Kondongpu.Presentation.API.Controllers
{
    /// <summary>
    /// กลุ่ม API Endpoint สำหรับจัดการประเภทการตรวจสุขภาพ (Checkup Types)
    /// </summary>
    public class CheckupTypesController : BaseController
    {
        /// <summary>
        /// API Endpoint สำหรับดึงข้อมูลรายการ ประเภทการตรวจสุขภาพ
        /// </summary>
        [HttpGet(Name = "GetCheckupTypeList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupTypeListViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CheckupTypeListViewModel>> GetCheckupTypeList()
        {            
            return Ok(await Mediator.Send(new GetCheckupTypeListQuery()));
        }
    }
}
