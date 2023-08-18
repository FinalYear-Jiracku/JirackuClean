using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Events;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Features.Queries.Events;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Helpers;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("projects/{id}")]
        public async Task<IActionResult> GetEventList(int id)
        {
            var findProject = await _mediator.Send(new GetProjectByIdQuery(id));
            if (findProject == null)
            {
                return StatusCode(400, "Project Does Not Exist");
            }
            var issueList = await _mediator.Send(new GetListDeadLineIssueQuery(id));
            var eventList = await _mediator.Send(new GetListEventQuery(id));
            var result = new {IssueList =  issueList, EventList = eventList};
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEventDetail(int id)
        {
            var findProject = await _mediator.Send(new GetProjectByIdQuery(id));
            if (findProject == null)
            {
                return StatusCode(400, "Project Does Not Exist");
            }
            var eventDetail = await _mediator.Send(new GetEventDetailQuery(id));
            return Ok(eventDetail);
        }

        [HttpGet]
        [Route("zoom")]
        public async Task<IActionResult> AuthZoom([FromQuery] string code)
        {
            return Ok(await _mediator.Send(new AuthZoomCommand(code)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var findEvent = await _mediator.Send(new GetEventDetailQuery(id));
            if (findEvent == null)
            {
                return StatusCode(400, "Event Does Not Exist");
            }
            await _mediator.Send(new DeleteEventCommand(id));
            return Ok();
        }
    }
}
