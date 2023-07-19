using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.Features.Queries.Pages;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Features.Queries.Users;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("accept/{inviteToken}")]
        public async Task<IActionResult> AcceptEmail(string inviteToken)
        {
            await _mediator.Send(new AcceptEmailQuery(inviteToken));
            return Ok();
        }
        [HttpGet]
        [Route("projects/{id}")]
        public async Task<IActionResult> DropdownUser(int id)
        {
            var findProject = await _mediator.Send(new GetProjectByIdQuery(id));
            if (findProject == null)
            {
                return StatusCode(400, "Project Does Not Exist");
            }
            var userList = await _mediator.Send(new GetListUserQuery(id));
            return Ok(userList);
        }
    }
}
