using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Features.Commands;
using NotificationServices.Application.Features.Queries;
using NotificationServices.Shared.Pagination.Filter;
using NotificationServices.Shared.Pagination.Helpers;
using NotificationServices.Shared.Pagination.Uris;

namespace NotificationServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        public MessagesController(IMediator mediator, IUriService uriService)
        {
            _mediator = mediator;
            _uriService = uriService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MessageList(int id)
        {
            var route = Request.Path.Value;
            var messageList = await _mediator.Send(new GetListMessageQuery(id));
            //var pagedResponse = PaginationHelper.CreatePagedReponse<MessageDTO>(projectList.Item1, projectList.Item2, projectList.Item3, _uriService, route);
            return Ok(messageList);
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
