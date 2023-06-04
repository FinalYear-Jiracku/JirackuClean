using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Features.Queries.Statuses;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IssuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET API
        [HttpGet]
        [Route("sprints/{id}")]
        public async Task<IActionResult> GetIssueList(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var issueList = await _mediator.Send(new GetIssueListQuery(id));
            return Ok(issueList);
        }

        [HttpGet]
        [Route("count/sprints/{id}")]
        public async Task<IActionResult> CountIssueNotCompleted(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var issue = await _mediator.Send(new CountIssueNotCompletedQuery(id));
            return Ok(issue);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueDetail(int id)
        {
            var status = await _mediator.Send(new GetIssueByIdQuery(id));
            if (status == null)
            {
                return StatusCode(400, "Issue Does Not Exist");
            }
            return Ok(status);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueCommand issueCommand)
        {
            await _mediator.Send(issueCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateIssue([FromBody] UpdateIssueCommand issueCommand)
        {
            await _mediator.Send(issueCommand);
            return Ok();
        }

        [HttpPut]
        [Route("sprints/{id}")]
        public async Task<IActionResult> CompleteIssue([FromQuery] int sprintToUpdate, int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            await _mediator.Send(new CompleteIssueCommand(id, sprintToUpdate));
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var findIssue = await _mediator.Send(new GetIssueByIdQuery(id));
            if (findIssue == null)
            {
                return StatusCode(400, "Issue Does Not Exist");
            }
            await _mediator.Send(new DeleteIssueCommand(id));
            return Ok();
        }
        #endregion
    }
}
