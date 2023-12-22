using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserServices.Application.Features.Commands.SMS;

namespace UserServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmssController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SmssController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody]SendSmsCodeCommand  command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifySmsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPut("disable/{id}")]
        public async Task<IActionResult> Disable(int id)
        {
            return Ok(await _mediator.Send(new DisableCodeSmsCommand(id)));
        }
    }
}
