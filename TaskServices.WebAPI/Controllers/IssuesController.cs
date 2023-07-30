using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Helpers;
using TaskServices.Shared.Pagination.Uris;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        public IssuesController(IMediator mediator, IUriService uriService)
        {
            _mediator = mediator;
            _uriService = uriService;
        }

        #region GET API
        [HttpGet]
        [Route("sprints/{id}")]
        public async Task<IActionResult> GetIssueList([FromQuery] PaginationFilter filter, int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var route = Request.Path.Value;
            var issueList = await _mediator.Send(new GetIssueListQuery(id,filter));
            var sprintName = await _mediator.Send(new GetSprintByIdQuery(id));
            var pagedResponse = PaginationHelper.CreatePagedReponse<IssueDTO>(issueList.Item1, issueList.Item2, issueList.Item3, _uriService, route);
            var result = new { sprintName.Name, Issues = pagedResponse };
            return Ok(result);
        }

        [HttpGet]
        [Route("deadline")]
        public async Task<IActionResult> CheckDeadline()
        {
            var issueList = await _mediator.Send(new CheckDeadlineIssueQuery());
            return Ok(issueList);
        }

        [HttpGet]
        [Route("uncomplete/sprints/{id}")]
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

        [HttpGet]
        [Route("complete/sprints/{id}")]
        public async Task<IActionResult> CountIssueCompleted(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var issue = await _mediator.Send(new CountIssueCompletedQuery(id));
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
        public async Task<IActionResult> UpdateIssue([FromForm] UpdateIssueCommand issueCommand)
        {
            await _mediator.Send(issueCommand);
            return Ok();
        }

        [HttpPut]
        [Route("order")]
        public async Task<IActionResult> UpdateOrderIssue([FromBody] UpdateOrderIssueCommand issueCommand)
        {
            await _mediator.Send(issueCommand);
            return Ok();
        }

        [HttpPut]
        [Route("dnd")]
        public async Task<IActionResult> UpdateDndIssue([FromBody] DndIssueCommand issueCommand)
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

        [HttpDelete("attachment/{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var findAttachment = await _mediator.Send(new DeleteAttachmentComand(id));
            if (findAttachment == null)
            {
                return StatusCode(400, "Attachment Does Not Exist");
            }
            return Ok();
        }
        #endregion
    }
}
