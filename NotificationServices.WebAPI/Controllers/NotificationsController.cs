using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationServices.Application.Features.Commands;
using NotificationServices.Application.Features.Queries;

namespace NotificationServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] SendEmaiCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> NotificationList(int id)
        {
            var listNoti = await _mediator.Send(new GetListNotificationQueries(id));
            return Ok(listNoti);
        }
    }
}
