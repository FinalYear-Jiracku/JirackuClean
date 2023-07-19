using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.Features.Commands.Comments;
using TaskServices.Application.Features.Queries.Comments;
using TaskServices.Application.Features.Queries.Projects;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET API
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentDetail(int id)
        {
            var comment = await _mediator.Send(new GetCommentByIdQuery(id));
            if (comment == null)
            {
                return StatusCode(400, "Comment Does Not Exist");
            }
            return Ok(comment);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand commentCommand)
        {
            await _mediator.Send(commentCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand commentCommand)
        {
            await _mediator.Send(commentCommand);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var findComment = await _mediator.Send(new GetCommentByIdQuery(id));
            if (findComment == null)
            {
                return StatusCode(400, "Column Does Not Exist");
            }
            await _mediator.Send(new DeleleCommentCommand(id));
            return Ok();
        }
        #endregion
    }
}
