using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Features.Queries.Statuses;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET API
        [HttpGet]
        [Route("sprints/{id}")]
        public async Task<IActionResult> GetStatusList(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var statusList = await _mediator.Send(new GetStatusListQuery(id));
            return Ok(statusList);
        }
        [HttpGet]
        [Route("data/sprints/{id}")]
        public async Task<IActionResult> GetDataStatusList(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var statusList = await _mediator.Send(new GetDataStatusListQuery(id));
            return Ok(statusList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusDetail(int id)
        {
            var status = await _mediator.Send(new GetStatusByIdQuery(id));
            if (status == null)
            {
                return StatusCode(400, "Status Does Not Exist");
            }
            return Ok(status);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusCommand statusCommand)
        {
            var checkStatusName = await _mediator.Send(new CheckStatusNameQuery(statusCommand.Name));
            if (checkStatusName)
            {
                return StatusCode(400, "Status Name already Exist");
            }
            await _mediator.Send(statusCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusCommand statusCommand)
        {
            var checkStatusName = await _mediator.Send(new CheckStatusNameQuery(statusCommand.Id, statusCommand.Name));
            if (checkStatusName)
            {
                return StatusCode(400, "Status Name already Exist");
            }
            await _mediator.Send(statusCommand);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var findStatus = await _mediator.Send(new GetStatusByIdQuery(id));
            if (findStatus == null)
            {
                return StatusCode(400, "Status Does Not Exist");
            }
            await _mediator.Send(new DeleteStatusCommand(id));
            return Ok();
        }
        #endregion
    }
}
