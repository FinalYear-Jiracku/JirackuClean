using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserServices.Application.Features.Commands;
using UserServices.Application.Features.Commands.QRCode;

namespace UserServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotpsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TotpsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateQRCodeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyQRCodeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] ValidateQRCodeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPut("disable/{id}")]
        public async Task<IActionResult> Disable(int id)
        {
            return Ok(await _mediator.Send(new DisableOTPCommand(id)));
        }
    }
}
