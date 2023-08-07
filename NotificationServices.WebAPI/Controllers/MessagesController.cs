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
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> MessageDetail(int id)
        {
            var message = await _mediator.Send(new GetMessageDetailQuery(id));
            return Ok(message);
        }

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            
            await _mediator.Send(new DeleteMessageCommand(id));
            return Ok();
        }
        #endregion
    }
}
