using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Common.Models;
using Kondongpu.Application.Features.Checkups.Commands.Create;
using Kondongpu.Application.Features.Checkups.Commands.Delete;
using Kondongpu.Application.Features.Checkups.Commands.Update;
using Kondongpu.Application.Features.Checkups.Queries;
using Kondongpu.Application.Features.Checkups.ViewModels;

namespace Kondongpu.Presentation.API.Controllers
{
    /// <summary>
    /// กลุ่ม API Endpoint สำหรับจัดการข้อมูลการตรวจสุขภาพ (Checkups)
    /// </summary>
    public class CheckupsController : BaseController
    {
        /// <summary>
        /// API Endpoint สำหรับเพิ่ม Checkup
        /// </summary>
        [HttpPost(Name = "CreateCheckup")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreateCheckup([FromBody] CreateCheckupCommand command)
        {
            var id = await Mediator.Send(command);
            return Created($"{Request.Path.Value}/{id}", id);
        }

        /// <summary>
        /// API Endpoint สำหรับอับเดต Checkup
        /// </summary>
        [HttpPut("{id}", Name = "UpdateCheckup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCheckup(int id, [FromBody] UpdateCheckupCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// API Endpoint สำหรับอับเดต Finalized status
        /// </summary>
        [HttpPut("Finalized", Name = "UpdateFinalizeStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateFinalizeStatus([FromBody] UpdateFinalizeStatusCommand command)
        {      
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// API Endpoint สำหรับ save final checkup
        /// </summary>
        [HttpPut("{id}/final", Name = "SaveFinalCheckup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> SaveFinalCheckup(int id, [FromBody] SaveCheckupFinalReportCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// API Endpoint สำหรับอับเดต Checkup ที่มาจาก WorkerService
        /// </summary>
        [HttpPut("{id}/sync/worker", Name = "UpdateCheckupFromWorker")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCheckupFromWorker(int id, [FromBody] UpdateCheckupFromWorkerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// API Endpoint สำหรับอับเดต Checkup ว่าผลแล็ป เอ็กซเรย์ออกครบหรือยัง ที่มาจาก WorkerService
        /// </summary>
        [HttpPut("synclabxray/worker", Name = "UpdateCheckupLabXrayFromWorker")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCheckupLabXrayFromWorker([FromBody] UpsertCheckupLabXrayIsResultReadyListCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// API Endpoint สำหรับอับเดต Checkup ว่าออเดอร์ Lab Xray Service มีอัพเดต ที่มาจาก WorkerService
        /// </summary>
        [HttpPut("synclabxrayserviceorder/worker", Name = "UpdateCheckupLabXrayOrderListCommand")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCheckupLabXrayOrderFromWorker([FromBody] UpdateCheckupLabXrayOrderListCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        //------------ Sync ------------
        
        /// <summary>
        /// API Endpoint สำหรับอับเดต Checkup ที่มาจาก Sync Data manual
        /// </summary>
        [HttpPut("{id}/sync/sync", Name = "UpdateCheckupFromSync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCheckupFromSync(int id, [FromBody] UpdateCheckupFromSyncCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        ///// <summary>
        ///// API Endpoint สำหรับอับเดต Checkup ว่าผลแล็ป เอ็กซเรย์ออกครบหรือยัง ที่มาจาก Sync Data manual
        ///// </summary>
        //[HttpPut("synclabxray/sync", Name = "UpdateCheckupLabXrayFromSync")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<IActionResult> UpdateCheckupLabXrayFromSync([FromBody] UpsertCheckupLabXrayIsResultReadyListCommand command)
        //{
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        ///// <summary>
        ///// API Endpoint สำหรับอับเดต Checkup ว่าออเดอร์ Lab Xray Service มีอัพเดต ที่มาจาก Sync Data manual
        ///// </summary>
        //[HttpPut("synclabxrayserviceorder/sync", Name = "UpdateCheckupLabXrayOrderListCommand")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<IActionResult> UpdateCheckupLabXrayOrderFromSync([FromBody] UpdateCheckupLabXrayOrderListCommand command)
        //{
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        //---------------end sync ---------------



        /// <summary>
        /// API Endpoint สำหรับลบข้อมูลCheckupด้วย Id
        /// </summary>
        [HttpDelete("{id}", Name = "DeleteCheckup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteCheckup(int id)
        {
            await Mediator.Send(new DeleteCheckupCommand { Id = id });
            return NoContent();
        }

        /// <summary>
        /// API Endpoint สำหรับดึงข้อมูล Checkup จาก Id
        /// </summary>
        [HttpGet("{id}", Name = "GetCheckup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CheckupViewModel>> GetCheckup(int id)
        {
            return Ok(await Mediator.Send(new GetCheckupQuery { Id = id }));
        }

        /// <summary>
        /// API Endpoint สำหรับดึงข้อมูล Checkup จาก Visit Number
        /// </summary>
        [HttpGet("visits/{visitNumber}", Name = "GetCheckupByVisitNumber")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CheckupViewModel>> GetCheckupByVisitNumber(string visitNumber)
        {
            return Ok(await Mediator.Send(new GetCheckupByVisitNumberQuery { VisitNumber = visitNumber }));
        }

        /// <summary>
        /// API Endpoint สำหรับดึงข้อมูลรายการ Checkup โดยระบุ StartDate และ EndDate
        /// </summary>
        /// <summary>
        /// API Endpoint สำหรับดึงข้อมูลรายการ Checkup โดยระบุ StartDate และ EndDate
        /// </summary>
        [HttpGet(Name = "GetCheckupList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupListViewModel))]
        public async Task<ActionResult<CheckupListViewModel>> GetCheckupList([FromQuery] GetCheckupListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// API Endpoint สำหรับดึงข้อมูลรายการ Checkup โดยระบุ StartDate และ EndDate และ HospitalNumber
        /// </summary>
        /// <summary>
        /// API Endpoint สำหรับดึงข้อมูลรายการ Checkup โดยระบุ StartDate และ EndDate และ HospitalNumber
        /// </summary>
        [HttpGet("hn" , Name = " GetCheckupListByHospitalNumber")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckupListViewModel))]
        public async Task<ActionResult<CheckupListViewModel>> GetCheckupListByHospitalNumber([FromQuery] GetCheckupListByHospitalNumberQuery query)
        {
            return Ok(await Mediator.Send(query));
        }


        /// <summary>
        /// API Endpoint สำหรับค้นหาข้อมูลCheckup ด้วยคำค้น ซึ่งจะ SearchTerm, StartDate, EndDate, Page และ Length
        /// </summary>
        [HttpGet("paginated", Name = "GetCheckupPaginatedList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<CheckupViewModel>))]
        public async Task<ActionResult<PaginatedList<CheckupViewModel>>> GetCheckupPaginatedList([FromQuery] GetCheckupPaginatedListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// API Endpoint สำหรับค้นหาข้อมูลCheckup ด้วยคำค้น ซึ่งจะ SearchTerm, StartDate, EndDate, Page และ Length
        /// </summary>
        [HttpGet("search", Name = "SearchCheckupList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<CheckupViewModel>))]
        public async Task<ActionResult<PaginatedList<CheckupViewModel>>> SearchCheckupList([FromQuery] SearchCheckupListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// API Endpoint สำหรับค้นหารายการตรวจของพนักงาน โดยค้นหาจาก Employee id 
        /// </summary>
        [HttpGet("employee", Name = "GetCheckupListByEmployeeId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<CheckupViewModel>))]
        public async Task<ActionResult<PaginatedList<CheckupViewModel>>> GetCheckupListByEmployeeId([FromQuery] GetCheckupListByEmployeeIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }



    }
}
