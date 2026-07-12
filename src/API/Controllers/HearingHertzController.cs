using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.HearingHertzs.Queries.Get;
using Kondongpu.Application.Features.HearingHertzs.ViewModel;

namespace Kondongpu.Presentation.API.Controllers;

/// <summary>
/// กลุ่ม API Endpoint สำหรับจัดการข้อมูลความถี่การตรวจการได้ยิน (Hearing Hertz)
/// </summary>
public class HearingHertzController : BaseController
{
    /// <summary>
    /// API Endpoint สำหรับดึงข้อมูลรายการ Hearing
    /// </summary>
    [HttpGet(Name = "GetHearingHertzList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<HearingHertzViewModel>))]
    public async Task<ActionResult<PaginatedList<HearingHertzViewModel>>> GetHearingList([FromQuery] GetHearingHertzListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
