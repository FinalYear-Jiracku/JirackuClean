using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Commands.Notes;
using TaskServices.Application.Features.Queries.Notes;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET API
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteDetail(int id)
        {
            var note = await _mediator.Send(new GetNoteByIdQuery(id));
            if (note == null)
            {
                return StatusCode(400, "Note Does Not Exist");
            }
            return Ok(note);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteCommand noteCommand)
        {
            await _mediator.Send(noteCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] UpdateNoteCommand noteCommand)
        {
            await _mediator.Send(noteCommand);
            return Ok();
        }

        [HttpPut]
        [Route("order")]
        public async Task<IActionResult> UpdateOrderIssue([FromBody] UpdateOrderIssueCommand issueCommand)
        {
            await _mediator.Send(issueCommand);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var findNote = await _mediator.Send(new GetNoteByIdQuery(id));
            if (findNote == null)
            {
                return StatusCode(400, "Note Does Not Exist");
            }
            await _mediator.Send(new DeleteNoteCommand(id));
            return Ok();
        }
        #endregion
    }
}
