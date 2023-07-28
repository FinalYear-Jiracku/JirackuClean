using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Helpers;
using TaskServices.Shared.Pagination.Uris;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        public StatusesController(IMediator mediator, IUriService uriService)
        {
            _mediator = mediator;
            _uriService = uriService;
        }

        #region GET API
        [HttpGet]
        [Route("data/sprints/{id}")]
        public async Task<IActionResult> GetDataStatusList([FromQuery] PaginationFilter filter, int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var route = Request.Path.Value;
            var statusList = await _mediator.Send(new GetDataStatusListQuery(id, filter));
            var pagedResponse = PaginationHelper.CreatePagedReponse<DataStatusDTO>(statusList.Item1, statusList.Item2, statusList.Item3, _uriService, route);
            return Ok(pagedResponse);
        }

        [HttpGet]
        [Route("sprints/{id}")]
        public async Task<IActionResult> DropdownStatusList(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var statusList = await _mediator.Send(new DropdownStatusListQuery(id));
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
            var checkStatusName = await _mediator.Send(new CheckStatusNameQuery(statusCommand.Name,statusCommand.SprintId));
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
            var checkStatusName = await _mediator.Send(new CheckStatusNameQuery(statusCommand.Id, statusCommand.Name, statusCommand.SprintId));
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
