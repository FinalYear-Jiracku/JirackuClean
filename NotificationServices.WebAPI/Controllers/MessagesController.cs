using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationServices.Application.Features.Commands;
using NotificationServices.Application.Features.Queries;

namespace NotificationServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MessageList(int id)
        {
            var listMessage = await _mediator.Send(new GetListMessageQuery(id));
            return Ok(listMessage);
        }
    }
}
