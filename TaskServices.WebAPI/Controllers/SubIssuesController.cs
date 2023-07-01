using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.Features.Commands.SubIssues;
using TaskServices.Application.Features.Queries.SubIssues;
namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubIssuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubIssuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET API
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubIssueDetail(int id)
        {
            var subIssue = await _mediator.Send(new GetSubIssueByIdQuery(id));
            if (subIssue == null)
            {
                return StatusCode(400, "SubIssue Does Not Exist");
            }
            return Ok(subIssue);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateSubIssue([FromBody] CreateSubIssueCommand subIssueCommand)
        {
            await _mediator.Send(subIssueCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateSubIssue([FromBody] UpdateSubIssueCommand subIssueCommand)
        {
            await _mediator.Send(subIssueCommand);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteSubIssue(int id)
        {
            var findIssue = await _mediator.Send(new GetSubIssueByIdQuery(id));
            if (findIssue == null)
            {
                return StatusCode(400, "SubIssue Does Not Exist");
            }
            await _mediator.Send(new DeleteSubIssueCommand(id));
            return Ok();
        }
        #endregion
    }
}
