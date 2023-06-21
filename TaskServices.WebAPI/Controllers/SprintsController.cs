using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Helpers;
using TaskServices.Shared.Pagination.Uris;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        public SprintsController(IMediator mediator, IUriService uriService)
        {
            _mediator = mediator;
            _uriService = uriService;
        }

        #region GET API
        [HttpGet]
        [Route("data/projects/{id}")]
        public async Task<IActionResult> GetSprintList([FromQuery] PaginationFilter filter, int id)
        {
            var findProject = await _mediator.Send(new GetProjectByIdQuery(id));
            if (findProject == null)
            {
                return StatusCode(400, "Project Does Not Exist");
            }
            var route = Request.Path.Value;
            var sprintList = await _mediator.Send(new GetSprintListQuery(filter, id));
            var projectName = await _mediator.Send(new GetProjectByIdQuery(id));
            var pagedResponse = PaginationHelper.CreatePagedReponse<SprintDTO>(sprintList.Item1, sprintList.Item2, sprintList.Item3, _uriService, route);
            var result = new { projectName.Name, Sprints = pagedResponse };
            return Ok(result);
        }

        [HttpGet]
        [Route("projects/{id}")]
        public async Task<IActionResult> DropdownSprint(int id)
        {
            var findProject = await _mediator.Send(new GetProjectByIdQuery(id));
            if (findProject == null)
            {
                return StatusCode(400, "Project Does Not Exist");
            }
            var sprintList = await _mediator.Send(new DropdownSprintListQuery(id));
            return Ok(sprintList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSprintDetail(int id)
        {
            var sprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (sprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            return Ok(sprint);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateSprint([FromBody] CreateSprintCommand sprintCommand)
        {
            var checkSprintName = await _mediator.Send(new CheckSprintNameQuery(sprintCommand.Name,sprintCommand.ProjectId));
            if (checkSprintName)
            {
                return StatusCode(400, "Sprint Name already Exist");
            }
            await _mediator.Send(sprintCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateSprint([FromBody] UpdateSprintCommand sprintCommand)
        {
            var checkSprintName = await _mediator.Send(new CheckSprintNameQuery(sprintCommand.Id, sprintCommand.Name, sprintCommand.ProjectId));
            if (checkSprintName)
            {
                return StatusCode(400, "Sprint Name already Exist");
            }
            await _mediator.Send(sprintCommand);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteSprint([FromQuery] int projectId, int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            await _mediator.Send(new DeleteSprintCommand(id, projectId));
            return Ok();
        }
        #endregion
    }
}
